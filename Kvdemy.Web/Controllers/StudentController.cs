using Krooti.Infrastructure.Services.Interfaces;
using Kvdemy.Core.Dtos.Helpers;
using Kvdemy.Core.Resourses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kvdemy.Web.Controllers
{
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

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<JsonResult> GetData(Pagination pagination, Query query)
		{
			var result = await _interfaceServices.userService.GetAllStudent(pagination, query);
			return Json(result);
		}
		[HttpGet]
		public async Task<IActionResult>  StudentInfo(string id)
		{
			var student = await _interfaceServices.userService.GetStudent(id);
			return View(student);
		}

		//[HttpGet]
		//public async Task<IActionResult> Delete(string id)
		//{
		//	await _interfaceServices.userService.Delete(id);
		//	return Ok(MyResults.DeleteSuccessResult());
		//}
	}
}