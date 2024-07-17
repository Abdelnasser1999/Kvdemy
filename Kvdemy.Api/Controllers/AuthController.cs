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

        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<IActionResult> StudentLogin([FromForm] LoginDto dto)
        //{

        //    var response = await _interfaceServices.authService.StudentLogin(dto);
        //    return Ok(response);

        //}

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromForm] LoginDto dto)
        {

            var response = await _interfaceServices.authService.Login(dto);
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
