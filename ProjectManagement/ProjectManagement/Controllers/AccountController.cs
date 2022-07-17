using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductsManagement.Model;
using ProductsManagement.Properties;
using ProductsManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationManager _authenticationManager;

        public AccountController(IAuthenticationManager authenticationManager)
        {
            _authenticationManager = authenticationManager;
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        {
            var result = await _authenticationManager.Register(userDTO);
            return Ok(new ApiResponse(Resource.REGISTER_SUCCESS));
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO userDTO)
        {
            var result = await _authenticationManager.Login(userDTO);
            return Ok(new ApiResponse(Resource.LOGIN_SUCCESS, null, new { Token = result }));
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            var result = await _authenticationManager.Logout();
            return Ok(new ApiResponse(result));
        }

        [HttpGet]
        [Route("ConfirmedEmail")]
        public async Task<IActionResult> ConfirmedEmail(Guid id, string code)
        {
            var result = await _authenticationManager.ConfirmedEmail(id, code);
            return Ok(new ApiResponse(result));
        }

        [HttpPost]
        [Route("forgotPassword")]
        public async Task<IActionResult> ForgotPassword(string mail)
        {
            var result = await _authenticationManager.ForgotPassword(mail);
            return Ok(new ApiResponse(result));
        }

        [HttpPost]
        [Route("resetPassword")]
        public async Task<IActionResult> ResetPassword([FromQuery] string code, [FromBody] ResetPassword resetPassword)
        {
            var result = await _authenticationManager.ResetPassword(code, resetPassword);
            return Ok(new ApiResponse(result));
        }
    }
}
