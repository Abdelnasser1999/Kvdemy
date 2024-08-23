using Kvdemy.Core.Dtos;
using Kvdemy.Core.Enums;
using Kvdemy.Core.ViewModels;
using Kvdemy.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Infrastructure.Services.Auth
{
    public interface IAuthService
    {

        Task<dynamic> StudentLogin(LoginDto dto);
        Task<dynamic> Login(LoginDto dto);
        Task<dynamic> CheckOtpCode(OtpCodeDto dto);
        //Task<dynamic> LoginAdmin(LoginAdminDto dto);
        Task<AccessTokenViewModel> GenerateAccessToken(User user, UserType userType);

        Task AddUserToRole(User user, UserType userType);
        //Task<dynamic> LogOut();

        Task<string> GetUserIdFromToken();
        Task<dynamic> SendVerificationCodeAsync(string phoneNumber);
        Task<dynamic> VerifyCodeAsync(string phoneNumber, string verificationCode);
        Task<dynamic> ResetPasswordAsync(string phoneNumber, string newPassword);
        Task<dynamic> ChangePasswordAsync(string userId, string currentPassword, string newPassword);
        //Task<dynamic> CreateAdmin(CreateAdminDto dto);

    }
}
