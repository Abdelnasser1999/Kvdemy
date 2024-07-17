using Kvdemy.Infrastructure.Services.Interfaces;
using Kvdemy.Core.Dtos;
using Kvdemy.Core.Resourses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Kvdemy.Infrastructure.Helpers;
using Kvdemy.Infrastructure.Services.Teachers;

namespace Kvdemy.API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TeacherController : BaseController
    {
        private readonly IInterfaceServices _interfaceServices;
        private readonly IStringLocalizer<Messages> _localizedMessages;
        protected string Language;

        public TeacherController(IInterfaceServices interfaceServices, IStringLocalizer<Messages> localizedMessages)
        {
            _interfaceServices = interfaceServices;
            _localizedMessages = localizedMessages;
            Language = Thread.CurrentThread.CurrentUICulture.Name;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterTeacher([FromForm] CreateTeacherDto dto)
        {
            var result = await _interfaceServices.userService.CreateTeacher(dto);
            return Ok(result);
        }

        [HttpPost("{id}/available-hours")]
        public async Task<IActionResult> AddAvailableHours(string id, [FromBody] AvailableHoursModel model)
        {
            var result = await _interfaceServices.teacherService.AddAvailableHoursAsync(id, model);
            return Ok(result);
        }

        [HttpPut("{id}/available-hours")]
        public async Task<IActionResult> UpdateAvailableHours(string id, [FromBody] AvailableHoursModel model)
        {
            var result = await _interfaceServices.teacherService.UpdateAvailableHoursAsync(id, model);
            return Ok(result);
        }
    }
}
