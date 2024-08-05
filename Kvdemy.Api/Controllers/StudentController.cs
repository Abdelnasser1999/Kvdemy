using Kvdemy.Infrastructure.Services.Interfaces;
using Kvdemy.Core.Dtos;
using Kvdemy.Core.Resourses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Kvdemy.Infrastructure.Helpers;
using Kvdemy.Infrastructure.Services.Teachers;
using System.Security.Claims;
using Kvdemy.Core.Constant;
using Kvdemy.Core.ViewModels;

namespace Kvdemy.API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class StudentController : BaseController
    {
        private readonly IInterfaceServices _interfaceServices;
        private readonly IStringLocalizer<Messages> _localizedMessages;
        protected string Language;

        public StudentController(IInterfaceServices interfaceServices, IStringLocalizer<Messages> localizedMessages)
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
        [HttpPost("{id}/generalprofile")]
        public async Task<IActionResult> UpdateStudent(string id , [FromForm] UpdateStudentGeneralInfoDto dto)
        {
            var result = await _interfaceServices.studentService.UpdateStudentAsync(id,dto);
            return Ok(result);
        }
        [HttpGet("{id}/generalprofile")]
        public async Task<IActionResult> GetProfile(string id)
        {
            var result = await _interfaceServices.studentService.GetProfileAsync(id);
            return Ok(result);
        }
        [HttpPost("{id}/profileImage")]
        public async Task<IActionResult> UpdateProfileImage(string id, [FromForm] ProfileImageDto imageDto)
        {
            var result = await _interfaceServices.studentService.UpdateProfileImageAsync(id, imageDto);
            return Ok(result);
        }

        [HttpGet("{id}/profileImage")]
        public async Task<IActionResult> GetProfileImage(string id)
        {
            var result = await _interfaceServices.studentService.GetProfileImageAsync(id);
            return Ok(result);
        }
        [HttpPost("{id}/level")]
        public async Task<IActionResult> UpdateAdditionalInformation(string id, string AdditionalInformation)
        {
            var result = await _interfaceServices.studentService.UpdateAdditionalInformationAsync(id, AdditionalInformation);
            return Ok(result);
        }

        [HttpGet("{id}/level")]
        public async Task<IActionResult> GetAdditionalInformation(string id)
        {
            try
            {
                var result = await _interfaceServices.studentService.GetAdditionalInformationAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseFailedViewModel(ex.Message));
            }

        }

        [HttpPost("{id}/Languages")]
        public async Task<IActionResult> UpdateStudentLanguages(string id, string StudentLanguages)
        {
            var result = await _interfaceServices.studentService.UpdateStudentLanguagesAsync(id, StudentLanguages);
            return Ok(result);
        }

        [HttpGet("{id}/Languages")]
        public async Task<IActionResult> GetStudentLanguages(string id)
        {
            try
            {
                var result = await _interfaceServices.studentService.GetStudentLanguagesAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseFailedViewModel(ex.Message));
            }

        }

    }
}
