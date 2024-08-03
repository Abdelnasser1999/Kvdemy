using Kvdemy.API.Controllers;
using Kvdemy.Core.Constants;
using Kvdemy.Core.Resourses;
using Kvdemy.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kvdemy.Api.Controllers
{

    [ApiController]
    [AllowAnonymous]
    public class SubjectController : BaseController
    {
        private readonly IInterfaceServices _interfaceServices;
        private readonly IStringLocalizer<Messages> _localizedMessages;
        protected string Language;

        public SubjectController(IInterfaceServices interfaceServices, IStringLocalizer<Messages> localizedMessages)
        {
            _interfaceServices = interfaceServices;
            _localizedMessages = localizedMessages;
            Language = Thread.CurrentThread.CurrentUICulture.Name;
        }

        [HttpGet]
        public async Task<IActionResult> SubjectTeachers([FromQuery] int subcategoryId, [FromQuery] string? searchText, [FromQuery] string? sortBy)
        {
            var result = await _interfaceServices.subjectService.SearchTeachersAsync(subcategoryId, searchText, sortBy);
            return Ok(result);
        }
    }
}
