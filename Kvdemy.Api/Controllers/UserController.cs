using Kvdemy.Infrastructure.Services.Interfaces;
using Kvdemy.API.Controllers;
using Kvdemy.Core.Dtos;
using Kvdemy.Core.Resourses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Kvdemy.Core.Constants;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Kvdemy.Api.Controllers
{
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IInterfaceServices _interfaceServices;
        private readonly IStringLocalizer<Messages> _localizedMessages;

        protected string Language;
        public UserController(IInterfaceServices interfaceServices, IStringLocalizer<Messages> localizedMessages)
        {
            _interfaceServices = interfaceServices;
            _localizedMessages = localizedMessages;
            Language = Thread.CurrentThread.CurrentUICulture.Name;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterStudent([FromForm] CreateStudentDto dto)
        {
            var result = await _interfaceServices.userService.CreateStudent(dto);

            return Ok(result);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterTeacher([FromForm] CreateTeacherDto dto)
        {
            var result = await _interfaceServices.userService.CreateTeacher(dto);
            return Ok(result);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetRegisterHelper()
        {
            var result = await _interfaceServices.userService.GetRegisterHelper();
            return Ok(GetRespons(result, MessageResults.GetSuccessResult()));
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetProfile(string userId)
        {
            var result = await _interfaceServices.userService.GetProfile(userId);
            return Ok(GetRespons(result, MessageResults.GetSuccessResult()));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DeleteAccount(string userId)
        {
            var result = await _interfaceServices.userService.DeleteAccount(userId);
            return Ok(GetRespons(result, MessageResults.GetSuccessResult()));
        }

    }
}
