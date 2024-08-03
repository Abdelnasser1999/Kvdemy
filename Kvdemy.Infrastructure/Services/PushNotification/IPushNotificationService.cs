using Kvdemy.Core.Dtos;
using Kvdemy.Core.Enums;
using Kvdemy.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Infrastructure.Services.PushNotification
{
    public interface IPushNotificationService
    {
        Task SendAsync(string userId, string message);

    }
}
