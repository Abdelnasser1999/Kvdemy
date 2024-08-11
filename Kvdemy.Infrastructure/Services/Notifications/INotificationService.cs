using Kvdemy.Core.Dtos;
using Kvdemy.Core.Enums;
using Kvdemy.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Infrastructure.Services.Notifications
{
    public interface INotificationService
    {
        Task SendNotificationAsync(string userId,string title, string message, NotificationType type);
        Task<dynamic> GetNotificationsForUserAsync(string userId);
        Task MarkNotificationAsReadAsync(int notificationId);

    }
}
