using Krooti.Infrastructure.Services.Payments;
using Kvdemy.API.Controllers;
using Kvdemy.Core.Constants;
using Kvdemy.Core.Dtos;
using Kvdemy.Core.Enums;
using Kvdemy.Core.Resourses;
using Kvdemy.Data.Models;
using Kvdemy.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using PayPalCheckoutSdk.Orders;
using System.Security.Policy;

namespace Kvdemy.Api.Controllers
{

    [ApiController]
    [AllowAnonymous]
    public class BookingController : BaseController
    {
        private readonly IInterfaceServices _interfaceServices;
        private readonly IStringLocalizer<Messages> _localizedMessages;
        protected string Language;

        public BookingController(IInterfaceServices interfaceServices, IStringLocalizer<Messages> localizedMessages)
        {
            _interfaceServices = interfaceServices;
            _localizedMessages = localizedMessages;
            Language = Thread.CurrentThread.CurrentUICulture.Name;
        }
        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromForm] CreateBookingDto bookingDto)
        {
            var result = await _interfaceServices.bookingService.CreateBookingAsync(bookingDto);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBookingStatus(int bookingId, [FromForm] UpdateBookingDto updateBookingDto)
        {
            var result = await _interfaceServices.bookingService.UpdateBookingStatusAsync(bookingId, updateBookingDto);
            return Ok(result);
        }

        [HttpGet("teacher/booking")]
        public async Task<IActionResult> GetBookingsForTeacher(string teacherId,  BookingStatus status)
        {
            var bookings = await _interfaceServices.bookingService.GetBookingsForTeacherAsync(teacherId , status);
            return Ok(bookings);
        }

        [HttpGet("student/booking")]
        public async Task<IActionResult> GetBookingsForStudent(string studentId)
        {
            var bookings = await _interfaceServices.bookingService.GetBookingsForStudentAsync(studentId);
            return Ok(bookings);
        }
        [HttpPost("create-payment")]
        public async Task<IActionResult> CreatePayment(int bookingId , decimal totalPrice, string return_url ,string cancel_url)
        {
            var paymentResponse = await _interfaceServices.paymentService.CreatePaymentAsync(totalPrice, return_url, cancel_url);
            var order = paymentResponse.Result<Order>(); // استخراج الكائن Order
            return Redirect(order.Links.FirstOrDefault(link => link.Rel == "approve")?.Href); // استخدم Links من Order
        }

        [HttpPost("payment/success")]
        public async Task<IActionResult> PaymentSuccess(string paymentId, string PayerID, int bookingId)
        {
            var result = await _interfaceServices.bookingService.SuccessBookingPayment(paymentId, PayerID,bookingId);
            return Ok(result);
        }

        [HttpPost("payment/cancel")]
        public async Task<IActionResult> PaymentCancel(int bookingId)
        {
            var result = await _interfaceServices.bookingService.FailedBookingPayment(bookingId);
            return Ok(result);
        }


        [Authorize]
        [HttpPost("booking/chat/send")]
        public async Task<IActionResult> SendMessage(int bookingId, [FromBody] string MessageContent)
        {
            string userId = await _interfaceServices.authService.GetUserIdFromToken();
            var booking = await _interfaceServices.bookingService.GetBookingById(bookingId);

            if (booking.StudentId != userId && booking.TeacherId != userId)
            {
                return Forbid();
            }

            var message = new BookingMessage
            {
                BookingId = bookingId,
                SenderId = userId,
                FileUrl = null,
                FileName = null,
                FileType = null,
                MessageContent = MessageContent,
                MessageType = MessageType.Text,
                CreatedAt = DateTime.UtcNow
            };

            await _interfaceServices.chatService.SaveMessageAsync(message);

            var result = await _interfaceServices.chatService.SendMessageAsync(bookingId.ToString(), MessageContent, userId);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("booking/chat/send-file")]
        public async Task<IActionResult> SendFile(int bookingId, IFormFile file)
        {
            string userId = await _interfaceServices.authService.GetUserIdFromToken();
            var booking = await _interfaceServices.bookingService.GetBookingById(bookingId);

            if (booking.StudentId != userId && booking.TeacherId != userId)
            {
                return Forbid();
            }

            var fileName = await _interfaceServices.fileService.SaveFileAsync(file, $"chat_{bookingId}");
            var fileUrl = Url.Content($"~/Chats/chat_{bookingId}/{fileName}");

            var message = new BookingMessage
            {
                BookingId = bookingId,
                SenderId = userId,
                FileUrl = fileUrl,
                FileName = fileName,
                FileType = file.ContentType,
                MessageContent = fileName,
                MessageType = MessageType.File,
                CreatedAt = DateTime.UtcNow
            };

            await _interfaceServices.chatService.SaveMessageAsync(message);
            await _interfaceServices.chatService.SendMessageAsync(bookingId.ToString(), fileName, userId);
            var result = await _interfaceServices.chatService.SendFileAsync(bookingId.ToString(), file, userId);
            return Ok(result);
        }
        [Authorize]
        [HttpGet("booking/chat/messages")]
        public async Task<IActionResult> GetMessages(int bookingId)
        {
            string userId = await _interfaceServices.authService.GetUserIdFromToken();
            var booking = await _interfaceServices.bookingService.GetBookingById(bookingId);

            // التحقق من أن المستخدم لديه صلاحية للوصول إلى الرسائل (طالب أو مدرس)
            if (booking.StudentId != userId && booking.TeacherId != userId)
            {
                return Forbid();
            }

            // جلب الرسائل الخاصة بالحجز
            var messages = await _interfaceServices.chatService.GetMessagesForBookingAsync(bookingId);

            return Ok(messages);
        }

    }
}
