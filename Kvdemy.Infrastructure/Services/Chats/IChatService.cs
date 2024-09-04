using Kvdemy.Core.Dtos;
using Kvdemy.Core.Enums;
using Kvdemy.Core.ViewModels;
using Kvdemy.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Infrastructure.Services.Chats
{
    public interface IChatService
    {
        Task<dynamic> SendMessageAsync(string bookingId, string message, string sender);
        Task SaveMessageAsync(BookingMessage chatMessage);
        Task<dynamic> SendFileAsync(string bookingId, IFormFile file, string sender);
        Task<dynamic> GetMessagesForBookingAsync(int bookingId); 

    }
}
