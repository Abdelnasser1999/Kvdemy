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
using Kvdemy.Infrastructure.Services.PushNotification;
using Kvdemy.Core.Constants;
using FirebaseAdmin.Messaging;
using System.Collections.Generic;


namespace Krooti.Infrastructure.Services.PushNotification
{
    public class PushNotificationService : IPushNotificationService
    {

        private readonly KvdemyDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IStringLocalizer<Messages> _localizedMessages;
        private readonly FirebaseMessaging _firebaseMessaging;

        public PushNotificationService(KvdemyDbContext dbContext, IMapper mapper
            , IFileService fileService, IStringLocalizer<Messages> localizedMessages, FirebaseMessaging firebaseMessaging)
        {
            _context = dbContext;
            _mapper = mapper;
            _fileService = fileService;
            _localizedMessages = localizedMessages;
            _firebaseMessaging = firebaseMessaging;

        }

        public async Task SendAsync(string userId, string message)
        {
            // احصل على FCM token من قاعدة البيانات أو من حيثما يتم تخزينه
            var userToken = await _context.Users
                .Where(u => u.Id == userId)
                .Select(u => u.FCMToken)
                .FirstOrDefaultAsync();

            //if (string.IsNullOrEmpty(userToken))
            //{
            //    throw new Exception("FCM token not found for user.");
            //}
            try
            {
                var messageToSend = new FirebaseAdmin.Messaging.Message()
                {
                    Token = userToken, // تأكد من أن هذا الحقل يحتوي على قيمة
                    Notification = new FirebaseAdmin.Messaging.Notification()
                    {
                        Title = "KVdemy",
                        Body = message
                    }
                };

                await _firebaseMessaging.SendAsync(messageToSend);

            }
            catch
            {

            }
        }

    }
}
