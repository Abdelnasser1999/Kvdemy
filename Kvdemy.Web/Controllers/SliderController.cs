using Kvdemy.Core.Constants;
using Kvdemy.Core.Dtos;
using Kvdemy.Core.Resourses;
using Kvdemy.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kvdemy.Web.Controllers
{
	public class SliderController : BaseController
    {
		private readonly IInterfaceServices _interfaceServices;
		private readonly IStringLocalizer<Messages> _localizedMessages;

		protected string Language;

		public SliderController(IInterfaceServices interfaceServices, IStringLocalizer<Messages> localizedMessages)
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

		public async Task<JsonResult> GetSliderData()
		{
			var result = await _interfaceServices.sliderService.GetAll();
			return Json(result);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateSliderDto dto)
		{
			if (ModelState.IsValid)
			{
				await _interfaceServices.sliderService.Create(dto);
				return Ok(MyResults.AddSuccessResult());
			}
			return View(dto);
		}

		[HttpGet]
		public async Task<IActionResult> Update(int id)
		{
			var user = await _interfaceServices.sliderService.Get(id);
			return View(user);
		}

		[HttpPost]
		public async Task<IActionResult> Update(UpdateSliderDto dto)
		{
			if (ModelState.IsValid)
			{
				await _interfaceServices.sliderService.Update(dto);
				return Ok(MyResults.EditSuccessResult());
			}
			return View(dto);
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			await _interfaceServices.sliderService.Delete(id);
			return Ok(MyResults.DeleteSuccessResult());
		}
	}
}