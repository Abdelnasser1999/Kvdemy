using Kvdemy.Core.Dtos;
using Kvdemy.Core.Enums;
using Kvdemy.Core.ViewModels;
using Kvdemy.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Infrastructure.Services.Bookings
{
    public interface IBookingService
    {
        Task<dynamic> CreateBookingAsync(CreateBookingDto bookingDto);
        Task<dynamic> UpdateBookingStatusAsync(int bookingId, UpdateBookingDto updateBookingDto);
        Task<dynamic> GetBookingsForTeacherAsync(string teacherId, BookingStatus status);
        Task<dynamic> GetBookingsForStudentAsync(string studentId);
        Task<dynamic> SuccessBookingPayment(string paymentId, string PayerID, int bookingId);
        Task<dynamic> FailedBookingPayment(int bookingId);
        Task<Booking> GetBookingById(int bookingId);
    }
}
