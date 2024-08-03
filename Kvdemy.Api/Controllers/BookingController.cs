using Kvdemy.API.Controllers;
using Kvdemy.Core.Constants;
using Kvdemy.Core.Dtos;
using Kvdemy.Core.Enums;
using Kvdemy.Core.Resourses;
using Kvdemy.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

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

        [HttpPut("{bookingId}")]
        public async Task<IActionResult> UpdateBookingStatus(int bookingId, [FromForm] UpdateBookingDto updateBookingDto)
        {
            var result = await _interfaceServices.bookingService.UpdateBookingStatusAsync(bookingId, updateBookingDto);
            return Ok(result);
        }

        [HttpGet("teacher/{teacherId}")]
        public async Task<IActionResult> GetBookingsForTeacher(string teacherId,  BookingStatus status)
        {
            var bookings = await _interfaceServices.bookingService.GetBookingsForTeacherAsync(teacherId , status);
            return Ok(bookings);
        }

        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetBookingsForStudent(string studentId)
        {
            var bookings = await _interfaceServices.bookingService.GetBookingsForStudentAsync(studentId);
            return Ok(bookings);
        }

    }
}
