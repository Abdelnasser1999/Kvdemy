using Kvdemy.API.Controllers;
using Kvdemy.Core.Constants;
using Kvdemy.Core.Dtos;
using Kvdemy.Core.Enums;
using Kvdemy.Core.Resourses;
using Kvdemy.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kvdemy.Api.Controllers
{

    [ApiController]
    [AllowAnonymous]
    public class ReportController : BaseController
    {
        private readonly IInterfaceServices _interfaceServices;
        private readonly IStringLocalizer<Messages> _localizedMessages;
        protected string Language;

        public ReportController(IInterfaceServices interfaceServices, IStringLocalizer<Messages> localizedMessages)
        {
            _interfaceServices = interfaceServices;
            _localizedMessages = localizedMessages;
            Language = Thread.CurrentThread.CurrentUICulture.Name;
        }
		[HttpPost("add")]
		public async Task<IActionResult> AddReport([FromForm] CreateReportDto dto)
		{
			var result = await _interfaceServices.reportService.AddReportAsync(dto);
			return Ok(result);
		}
		[HttpPost("mark-as-read")]
		public async Task<IActionResult> MarkAsRead([FromForm] int reportId)
		{
			var result = await _interfaceServices.reportService.MarkAsReadAsync(reportId);
			return Ok(result);
		}

	}
}
