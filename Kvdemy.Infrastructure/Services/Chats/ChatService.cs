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
using Newtonsoft.Json;
using Kvdemy.Infrastructure.Helpers;
using Kvdemy.Web.Data.Migrations;
using Kvdemy.Infrastructure.Services.Payments;
using Kvdemy.Infrastructure.Services.Chats;
using PusherServer;
using Microsoft.AspNetCore.Http;


namespace Krooti.Infrastructure.Services.Chats
{
    public class ChatService : IChatService
    {

        private readonly KvdemyDbContext _context;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
        private readonly IFileService _fileService;
        private readonly IStringLocalizer<Messages> _localizedMessages;
        private readonly Pusher _pusher;

        public ChatService(KvdemyDbContext dbContext, IMapper mapper
            , INotificationService notificationService, IStringLocalizer<Messages> localizedMessages , IFileService fileService)
        {
            _context = dbContext;
            _mapper = mapper;
            _notificationService = notificationService;
            _localizedMessages = localizedMessages;
            _fileService = fileService;
            var options = new PusherOptions
            {
                Cluster = "us2"
            };
            _pusher = new Pusher("1858340", "f4ba9f8b7dfba819b802", "0f4b8cea4991316cddff", options);

        }
        public async Task<dynamic> SendMessageAsync(string bookingId, string message, string sender)
        {
            var result = await _pusher.TriggerAsync(
                channelName: $"chat_{bookingId}",
                eventName: "new_message",
                data: new { message = message, sender = sender }
            );

            return new ApiResponseSuccessViewModel("success",result.ChannelAttributes.ToList());

        }

        public async Task<dynamic> SendFileAsync(string bookingId, IFormFile file, string sender)
        {
            var fileName = await _fileService.SaveFileAsync(file, $"chat_{bookingId}");
            var fileUrl = $"/Chats/chat_{bookingId}/{fileName}";

            var result = await _pusher.TriggerAsync(
                channelName: $"chat_{bookingId}",
                eventName: "new_file",
                data: new { fileUrl = fileUrl, fileName = fileName, sender = sender }
            );

            return new ApiResponseSuccessViewModel("success", result.ChannelAttributes.ToList());
        }
        public async Task SaveMessageAsync(BookingMessage chatMessage)
        {
            _context.BookingMessages.Add(chatMessage);
            await _context.SaveChangesAsync();
        }
        public async Task<dynamic> GetMessagesForBookingAsync(int bookingId)
        {
            var messages = await _context.BookingMessages
                                         .Where(m => m.BookingId == bookingId)
                                         .OrderBy(m => m.CreatedAt)
                                         .ToListAsync();
            return new ApiResponseSuccessViewModel("success", messages);
        }

    }
}
