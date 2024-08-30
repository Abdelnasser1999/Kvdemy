using Kvdemy.Core.Constants;
using Kvdemy.Core.Dtos;
using Kvdemy.Core.Dtos.Helpers;
using Kvdemy.Core.Resourses;
using Kvdemy.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kvdemy.Web.Controllers
{
	public class WalletRequestController : BaseController
    {
		private readonly IInterfaceServices _interfaceServices;
		private readonly IStringLocalizer<Messages> _localizedMessages;

		protected string Language;

		public WalletRequestController(IInterfaceServices interfaceServices, IStringLocalizer<Messages> localizedMessages)
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

		public async Task<JsonResult> GetWalletRequestData(Pagination pagination, Query query)
		{
			var result = await _interfaceServices.financeAccountService.GetAllWalletRequests(pagination, query);
			return Json(result);
		}


		[HttpGet]
		public async Task<IActionResult> Accept(int id)
		{
			await _interfaceServices.financeAccountService.AcceptWalletRequest(id);
			//return Ok(MyResults.AcceptResult());
			return Ok();
		}
		[HttpGet]
		public async Task<IActionResult> Reject(int id)
		{
			var userId = await _interfaceServices.financeAccountService.RejectWalletRequest(id);
			//CreateNotificationDto dto = new CreateNotificationDto();
			//dto.Title = "فشل شحن الرصيد";
			//dto.Description = "تم الغاء طلب شحن الرصيد";
			//dto.UserId = userId;
			//await _interfaceServices.notificationService.CreateNotifications(dto);
			//return Ok(MyResults.RejectResult());
			return Ok();
		}

	
	}
}