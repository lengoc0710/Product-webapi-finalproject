using ProductsManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsManagement.Services
{
    public interface IAuthenticationManager
    {
        Task<string> Login(LoginUserDTO userDTO);
        Task<string> CreateToken();
        Task<string> Logout();
        Task<bool> Register(UserDTO userDTO);
        Task<string> ConfirmedEmail(Guid id, string code);
        Task<string> ForgotPassword(string mail);
        Task<string> ResetPassword(string code, ResetPassword resetPassword);
    }
}
