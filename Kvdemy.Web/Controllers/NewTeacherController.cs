using Kvdemy.Infrastructure.Services.Interfaces;
using Kvdemy.Core.Constants;
using Kvdemy.Core.Dtos;
using Kvdemy.Core.Dtos.Helpers;
using Kvdemy.Core.Resourses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kvdemy.Web.Controllers
{
	public class NewTeacherController : BaseController
    {
		private readonly IInterfaceServices _interfaceServices;
		private readonly IStringLocalizer<Messages> _localizedMessages;

		protected string Language;

		public NewTeacherController(IInterfaceServices interfaceServices, IStringLocalizer<Messages> localizedMessages)
		{
			_interfaceServices = interfaceServices;
			_localizedMessages = localizedMessages;
			Language = Thread.CurrentThread.CurrentUICulture.Name;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<JsonResult> GetData(Pagination pagination, Query query)
		{
			var result = await _interfaceServices.userService.GetNewTeachers(pagination, query);
			return Json(result);
		}
		[HttpGet]
		public async Task<IActionResult>  TeacherInfo(string id)
		{
			var Teacher = await _interfaceServices.userService.GetTeacher(id);
			return View(Teacher);
		}

		[HttpGet]
		public async Task<IActionResult> Accept(string id)
		{
			await _interfaceServices.userService.AcceptTeacher(id);
			return Ok(MyResults.EditSuccessResult());
		}
		[HttpGet]
		public async Task<IActionResult> Reject(string id)
		{
			await _interfaceServices.userService.RejectTeacher(id);
            return Ok(MyResults.EditSuccessResult());
		}
	}
}