using Krooti.Infrastructure.Services.Payments;
using Kvdemy.API.Controllers;
using Kvdemy.Core.Constants;
using Kvdemy.Core.Dtos;
using Kvdemy.Core.Enums;
using Kvdemy.Core.Resourses;
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

        [HttpGet("payment/success")]
        public async Task<IActionResult> PaymentSuccess(string paymentId, string PayerID, int bookingId)
        {
            var result = _interfaceServices.bookingService.SuccessBookingPayment(paymentId, PayerID,bookingId);
            return Ok(result);
        }

        [HttpGet("payment/cancel")]
        public IActionResult PaymentCancel(int bookingId)
        {
            var result = _interfaceServices.bookingService.FailedBookingPayment(bookingId);
            return Ok(result);
        }

    }
}
