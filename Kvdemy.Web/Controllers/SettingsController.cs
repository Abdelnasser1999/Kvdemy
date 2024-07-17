using Kvdemy.Core.Constants;
using Kvdemy.Core.Dtos;
using Kvdemy.Core.Resourses;
using Kvdemy.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kvdemy.Web.Controllers
{
	public class SettingsController : BaseController
    {
		private readonly IInterfaceServices _interfaceServices;
		private readonly IStringLocalizer<Messages> _localizedMessages;

		protected string Language;

		public SettingsController(IInterfaceServices interfaceServices, IStringLocalizer<Messages> localizedMessages)
		{
			_interfaceServices = interfaceServices;
			_localizedMessages = localizedMessages;
			Language = Thread.CurrentThread.CurrentUICulture.Name;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> About()
		{
			var about = await _interfaceServices.settingsService.Get();
			return View(about);
		}

		[HttpPost]
		public async Task<IActionResult> About(UpdateSettingsDto dto)
		{
			if (ModelState.IsValid)
			{
				await _interfaceServices.settingsService.Update(dto);
				return Ok(MyResults.EditSuccessResult());
			}
			return View(dto);
		}

		[HttpGet]
		public async Task<IActionResult> Privacy()
		{
			var about = await _interfaceServices.settingsService.Get();
			return View(about);
		}

		[HttpPost]
		public async Task<IActionResult> Privacy(UpdateSettingsDto dto)
		{
			if (ModelState.IsValid)
			{
				await _interfaceServices.settingsService.Update(dto);
				return Ok(MyResults.EditSuccessResult());
			}
			return View(dto);
		}

		[HttpGet]
		public async Task<IActionResult> Terms()
		{
			var about = await _interfaceServices.settingsService.Get();
			return View(about);
		}

		[HttpPost]
		public async Task<IActionResult> Terms(UpdateSettingsDto dto)
		{
			if (ModelState.IsValid)
			{
				await _interfaceServices.settingsService.Update(dto);
				return Ok(MyResults.EditSuccessResult());
			}
			return View(dto);
		}

		[HttpGet]
		public async Task<IActionResult> Currencies()
		{
			var about = await _interfaceServices.settingsService.Get();
			return View(about);
		}

		[HttpPost]
		public async Task<IActionResult> Currencies(UpdateSettingsDto dto)
		{
			if (ModelState.IsValid)
			{
				await _interfaceServices.settingsService.Update(dto);
				return Ok(MyResults.EditSuccessResult());
			}
			return View(dto);
		}
	}
}