﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProductsManagement.Data;
using ProductsManagement.Model;
using ProductsManagement.Models;
using ProductsManagement.Properties;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProductsManagement.Services
{
  
        public class AuthenticationManager : IAuthenticationManager
        {
            private readonly UserManager<ApiUserSystem> _userManager;
            private readonly IConfiguration _configuration;
            private readonly IHttpContextAccessor _httpContextAccessor;
            private ApiUserSystem _user;
            private readonly IMapper _mapper;
            private readonly ISendMailService _emailSender;

            public AuthenticationManager(UserManager<ApiUserSystem> userManager,
                IConfiguration configuration,
                IHttpContextAccessor httpContextAccessor,
                IMapper mapper,
                ISendMailService emailSender)
            {
                _userManager = userManager;
                _configuration = configuration;
                _httpContextAccessor = httpContextAccessor;
                _mapper = mapper;
                _emailSender = emailSender;
            }

            public async Task<string> CreateToken()
            {
                var signingCredentials = GetSigningCredentials();
                var claims = await GetClaims();
                var token = GenerateTokenOptions(signingCredentials, claims);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }

            private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
            {
                var jwtSettings = _configuration.GetSection("Jwt");
                var expiration = DateTime.Now.AddMinutes(Convert.ToDouble(
                    jwtSettings.GetSection("lifetime").Value));

                var token = new JwtSecurityToken(
                    issuer: jwtSettings.GetSection("Issuer").Value,
                    claims: claims,
                    expires: expiration,
                    signingCredentials: signingCredentials
                    );

                return token;
            }

            private async Task<List<Claim>> GetClaims()
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, _user.UserName),
                new Claim("LoginFor", "Web")
            };

                var roles = await _userManager.GetRolesAsync(_user);

                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                return claims;
            }

            private SigningCredentials GetSigningCredentials()
            {
                var key = _configuration.GetSection("Jwt").GetSection("Key").Value;
                var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

                return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
            }

            public async Task<bool> Register(UserDTO userDTO)
            {
                var user = _mapper.Map<ApiUserSystem>(userDTO);
                user.UserName = userDTO.Email;
                var result = await _userManager.CreateAsync(user, userDTO.Password);

                if (!result.Succeeded)
                {
                    List<string> error = new List<string>();
                    foreach (var e in result.Errors)
                    {
                        error.Add(e.Description);
                    }
                    throw new BusinessException(error[0]);
                }
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = string.Format($"/api/Account/ConfirmedEmail?id={user.Id}&code={code}");

                await _emailSender.SendEmailAsync(userDTO.Email, Resource.SEND_MAIL_CONFIRMED,
                            string.Format(Resource.SEND_MAIL_CONFIRMED_BODY, callbackUrl));
                await _userManager.AddToRolesAsync(user, userDTO.Roles);

                return true;
            }

            public async Task<string> Login(LoginUserDTO userDTO)
            {
                _user = await _userManager.FindByNameAsync(userDTO.Email);
                var validPassword = await _userManager.CheckPasswordAsync(_user, userDTO.Password);
                if (_user != null && validPassword)
                {
                    return await CreateToken();
                }
                throw new BusinessException(Resource.LOGIN_FAIL);
            }

            public async Task<string> Logout()
            {
                var identity = (ClaimsIdentity)_httpContextAccessor.HttpContext.User.Identity;

                //Gets list of claims.
                IEnumerable<Claim> claims = identity.Claims;

                var usernameClaim = claims
                    .Where(x => x.Type == ClaimTypes.Name)
                    .FirstOrDefault();

                var user = await _userManager.FindByNameAsync(usernameClaim.Value);
                var result = await _userManager.RemoveAuthenticationTokenAsync(user, "Web", "Access");
                if (result.Succeeded)
                {
                    return Resource.LOGOUT_SUCCESS;
                }
                throw new BusinessException(Resource.LOGOUT_FAIL);
            }

            public async Task<string> ConfirmedEmail(Guid id, string key)
            {
                List<string> error = new List<string>();
                var user = await _userManager.FindByIdAsync(id.ToString());
                var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(key));
                var result = await _userManager.ConfirmEmailAsync(user, code);
                if (result.Succeeded)
                {

                    return Resource.COMFIRMED_SUCCESS;
                }
                else
                {
                    foreach (var e in result.Errors)
                    {
                        error.Add(e.Description);
                    }
                    throw new BusinessException(error[0]);
                }
            }

            public async Task<string> ForgotPassword(string mail)
            {
                var user = await _userManager.FindByEmailAsync(mail);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    return Resource.FORGOT_PASSWORD_SUCCESS;
                }
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = string.Format($"/api/Account/resetPassword?code={code}");
                await _emailSender.SendEmailAsync(user.Email, Resource.FORGOT_PASSWORD_BODY,
                        string.Format(Resource.FORGOT_PASSWORD_BODY, callbackUrl));
                throw new BusinessException(Resource.FORGOT_PASSWORD_SUCCESS);
            }

            public async Task<string> ResetPassword(string key, ResetPassword resetPassword)
            {
                if (key == null)
                {
                    throw new BusinessException(Resource.NOT_TOKEN);
                }

                var user = await _userManager.FindByEmailAsync(resetPassword.Email);
                if (user == null)
                {
                    throw new BusinessException(Resource.NOT_ACCOUNT);
                }
                var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(key));
                var result = await _userManager.ResetPasswordAsync(user, code, resetPassword.Password);
                List<string> error = new List<string>();
                if (result.Succeeded)
                {
                    return Resource.RESETPASSWORD_SUCCESS;
                }
                else
                {
                    foreach (var e in result.Errors)
                    {
                        error.Add(e.Description);
                    }
                }
                throw new BusinessException(error[0]);
            }
        }
    }
