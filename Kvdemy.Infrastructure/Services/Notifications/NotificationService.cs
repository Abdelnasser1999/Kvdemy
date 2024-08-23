using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Kvdemy.Web.Data;
using Kvdemy.Core.ViewModels;
using Kvdemy.Data.Models;
using Kvdemy.Core.Dtos;
using Kvdemy.Core.Exceptions;
using Kvdemy.Infrastructure.Services.Subject;
using Kvdemy.Core.Enums;
using Kvdemy.Core.Constant;
using Kvdemy.Core.Resourses;
using Kvdemy.Web.Services;
using Microsoft.Extensions.Localization;
using Kvdemy.Infrastructure.Services.Bookings;
using Kvdemy.Infrastructure.Services.Interfaces;
using Kvdemy.Infrastructure.Services.Notifications;
using Kvdemy.Infrastructure.Services.PushNotification;


namespace Krooti.Infrastructure.Services.Notifications
{
    public class NotificationService : INotificationService
    {

        private readonly KvdemyDbContext _context;
        private readonly IMapper _mapper;
        private readonly IPushNotificationService _pushNotificationService;
        private readonly IStringLocalizer<Messages> _localizedMessages;

        public NotificationService(KvdemyDbContext dbContext, IMapper mapper
            , IPushNotificationService pushNotificationService, IStringLocalizer<Messages> localizedMessages)
        {
            _context = dbContext;
            _mapper = mapper;
            _pushNotificationService = pushNotificationService;
            _localizedMessages = localizedMessages;
        }
        public async Task SendNotificationAsync(string userId,string title, string message, NotificationType type)
        {
            var notification = new Notification
            {
                UserId = userId,
                Title = title,
                Message = message,
                NotificationType = type,
                CreatedAt = DateTime.UtcNow
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            await _pushNotificationService.SendAsync(userId, message);
        }

        public async Task<dynamic> GetNotificationsForUserAsync(string userId)
        {
            var notifications = await _context.Notifications
                                              .Where(n => n.UserId == userId)
                                              .OrderByDescending(b => b.CreatedAt)
                                              .ToListAsync();

            var result =  _mapper.Map<List<NotificationViewModel>>(notifications);
            return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.DataSuccess], result);

        }

        public async Task MarkNotificationAsReadAsync(int notificationId)
        {
            var notification = await _context.Notifications.FindAsync(notificationId);
            if (notification != null)
            {
                notification.IsRead = true;
                await _context.SaveChangesAsync();
            }
        }

    }
}
