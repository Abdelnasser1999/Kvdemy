using Kvdemy.Infrastructure.Services.Interfaces;
using Kvdemy.Core.Constants;
using Kvdemy.Core.Dtos;
using Kvdemy.Core.Dtos.Helpers;
using Kvdemy.Core.Resourses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kvdemy.Web.Controllers
{
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

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<JsonResult> GetData(Pagination pagination, Query query)
		{
			var result = await _interfaceServices.userService.GetAllTeachers(pagination, query);
			return Json(result);
		}
		[HttpGet]
		public async Task<IActionResult>  TeacherInfo(string id)
		{
			var Teacher = await _interfaceServices.userService.GetTeacher(id);
			return View(Teacher);
		}
		[HttpGet]
		public async Task<IActionResult> Create()
		{
            ViewBag.Nationality = await _interfaceServices.userService.GetNationalities();
            return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromForm] CreateTeacherDto dto)
		{
			if (ModelState.IsValid)
			{
				await _interfaceServices.userService.CreateTeacher(dto);
				return Ok(MyResults.AddSuccessResult());
			}
            ViewBag.Nationality = await _interfaceServices.userService.GetNationalities();
            return View(dto);

		}

        [HttpGet]
        public async Task<IActionResult> FinanceAccount(string id)
        {
            var model = await _interfaceServices.teacherService.GetFinanceAccount(id);
            return View(model);
        }

        public async Task<IActionResult> GetTransactions(int id)
        {
            var result = await _interfaceServices.teacherService.GetTransactions(id);
            return Json(result);
        }


        [HttpGet]
		public async Task<IActionResult> Delete(string id)
		{
			await _interfaceServices.userService.Delete(id);
			return Ok(MyResults.DeleteSuccessResult());
		}
	}
}