using Krooti.Infrastructure.Services.Interfaces;
using Kvdemy.API.Controllers;
using Kvdemy.Core.Dtos;
using Kvdemy.Core.Resourses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

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
        public async Task<IActionResult> Register([FromForm] CreateStudentDto dto)
        {
            var result = await _interfaceServices.userService.CreateStudent(dto);

            return Ok(result);
        }

    }
}
