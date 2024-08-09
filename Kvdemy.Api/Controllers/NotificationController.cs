using Kvdemy.API.Controllers;
using Kvdemy.Core.Constant;
using Kvdemy.Core.Constants;
using Kvdemy.Core.Dtos;
using Kvdemy.Core.Enums;
using Kvdemy.Core.Resourses;
using Kvdemy.Core.ViewModels;
using Kvdemy.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Kvdemy.Api.Controllers
{

    [ApiController]
    [AllowAnonymous]
    public class NotificationController : BaseController
    {
        private readonly IInterfaceServices _interfaceServices;
        private readonly IStringLocalizer<Messages> _localizedMessages;
        protected string Language;

        public NotificationController(IInterfaceServices interfaceServices, IStringLocalizer<Messages> localizedMessages)
        {
            _interfaceServices = interfaceServices;
            _localizedMessages = localizedMessages;
            Language = Thread.CurrentThread.CurrentUICulture.Name;
        }

		[HttpPost("send")]
		public async Task<IActionResult> SendNotification([FromForm] string userId, [FromForm] string message, [FromForm] NotificationType type)
		{
			await _interfaceServices.notificationService.SendNotificationAsync(userId, message, type);
			return Ok(new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.ItemCreatedSuccss]));
		}

		[HttpGet("user")]
		public async Task<IActionResult> GetNotificationsForUser(string userId)
		{
			var result = await _interfaceServices.notificationService.GetNotificationsForUserAsync(userId);
			return Ok(result);
		}

		[HttpPost("mark-as-read")]
		public async Task<IActionResult> MarkNotificationAsRead(int notificationId)
		{
			await _interfaceServices.notificationService.MarkNotificationAsReadAsync(notificationId);
			return Ok(new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.ItemUpdatedSuccss]));
		}

	}
}
