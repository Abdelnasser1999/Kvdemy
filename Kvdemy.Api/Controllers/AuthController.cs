using Kvdemy.Infrastructure.Services.Interfaces;
using Kvdemy.Core.Dtos;
using Kvdemy.Core.Resourses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kvdemy.API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : BaseController
    {

        private readonly IInterfaceServices _interfaceServices;
        private readonly IStringLocalizer<Messages> _localizedMessages;
        protected string Language;

        public AuthController(IInterfaceServices interfaceServices, IStringLocalizer<Messages> localizedMessages)
        {
            _interfaceServices = interfaceServices;
            _localizedMessages = localizedMessages;
            Language = Thread.CurrentThread.CurrentUICulture.Name;
        }

        [HttpPost]
        public async Task<IActionResult> SendVerificationCode([FromForm] string email)
        {
            var response = await _interfaceServices.authService.SendVerificationCodeAsync(email);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> VerifyCode([FromForm] string email, [FromForm] string verificationCode)
        {
            var response = await _interfaceServices.authService.VerifyCodeAsync(email, verificationCode);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword([FromForm] string email, [FromForm] string newPassword)
        {
            var response = await _interfaceServices.authService.ResetPasswordAsync(email, newPassword);
            return Ok(response);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromForm] LoginDto dto)
        {

            var response = await _interfaceServices.authService.Login(dto);
            return Ok(response);

        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword([FromForm] string userId, [FromForm] string currentPassword, [FromForm] string newPassword)
        {
            var response = await _interfaceServices.authService.ChangePasswordAsync(userId, currentPassword, newPassword);
            return Ok(response);

        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CheckOtpCode([FromForm] OtpCodeDto dto)
        {

            var response = await _interfaceServices.authService.CheckOtpCode(dto);
            return Ok(response);
        }

        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<IActionResult> LoginAdmin([FromForm] LoginAdminDto dto)
        //{

        //    var response = await _interfaceServices.authService.LoginAdmin(dto);
        //    return Ok(response);
        //}
        //[HttpPost]

        //public async Task<IActionResult> CreateAdmin(CreateAdminDto dto)
        //{

        //    var response = await _interfaceServices.authService.CreateAdmin(dto);
        //    return Ok(response);
        //}


        //[HttpPost]

        //public async Task<IActionResult> Logout()
        //{

        //    var response = await _interfaceServices.authService.LogOut();
        //    return Ok(response);
        //}



    }
}
