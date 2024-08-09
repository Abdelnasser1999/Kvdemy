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


namespace Krooti.Infrastructure.Services.Bookings
{
    public class BookingService : IBookingService
    {

        private readonly KvdemyDbContext _context;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
        private readonly IStringLocalizer<Messages> _localizedMessages;

        public BookingService(KvdemyDbContext dbContext, IMapper mapper
            , INotificationService notificationService, IStringLocalizer<Messages> localizedMessages)
        {
            _context = dbContext;
            _mapper = mapper;
            _notificationService = notificationService;
            _localizedMessages = localizedMessages;
        }
        public async Task<dynamic> CreateBookingAsync(CreateBookingDto bookingDto)
        {
            // الحصول على معلومات المدرس
            var teacher = await _context.Users.FindAsync(bookingDto.TeacherId);
            if (teacher == null || teacher.UserType != UserType.Teacher)
                return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.ItemNotFound]);

            // التحقق من الساعات المتاحة
            if (string.IsNullOrEmpty(teacher.AvailableHours))
                return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.TeacherUnavailable]);

            var availableHoursModel = JsonConvert.DeserializeObject<AvailableHoursModel>(teacher.AvailableHours);

            // التحقق من أن AvailableHours ليس null
            if (availableHoursModel == null || availableHoursModel.AvailableHours == null)
                return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.TeacherUnavailable]);

            var bookingDateTime = bookingDto.StartTime;
            var dayOfWeek = bookingDateTime.DayOfWeek.ToString();

            if (!availableHoursModel.AvailableHours.ContainsKey(dayOfWeek))
                return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.TeacherUnavailableOnThisDay]);

            var availableTimes = availableHoursModel.AvailableHours[dayOfWeek];
            var isAvailable = availableTimes.Any(timeRange =>
            {
                var fromTime = DateTime.Parse(timeRange.From).TimeOfDay;
                var toTime = DateTime.Parse(timeRange.To).TimeOfDay;
                return bookingDateTime.TimeOfDay >= fromTime && bookingDateTime.TimeOfDay < toTime;
            });

            if (!isAvailable)
                return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.BookingNotAvailable]);

            // التحقق من عدم تعارض الحجز مع حجوزات أخرى
            var conflictingBookings = await _context.Bookings
                .Where(b => b.TeacherId == bookingDto.TeacherId &&
                            b.Status != BookingStatus.Cancelled &&
                            bookingDto.StartTime < b.EndTime &&
                            bookingDto.EndTime > b.StartTime)
                .ToListAsync();

            if (conflictingBookings.Any())
            {
                return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.BookingConflict]);
            }

            // إذا كانت الشروط مستوفية، نقوم بإضافة الحجز الجديد
            var booking = _mapper.Map<Booking>(bookingDto);
            booking.Status = BookingStatus.Pending;
            booking.CreatedAt = DateTime.UtcNow;

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            // إرسال إشعار للمدرس
            await _notificationService.SendNotificationAsync(booking.TeacherId, _localizedMessages[MessagesKey.NewBooking], NotificationType.Booking);

            return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.BookingCreatedSuccess]);
        }

        public async Task<dynamic> UpdateBookingStatusAsync(int bookingId, UpdateBookingDto updateBookingDto)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);
            if (booking == null)
                return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.BookingNotFound]);

            booking.Status = updateBookingDto.status;
            booking.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            var message = _localizedMessages[MessagesKey.BookingUpdatedSuccess];
            switch (updateBookingDto.status)
            {
                case BookingStatus.Approved:
                    message = _localizedMessages[MessagesKey.BookingApproved];
                    break;
                case BookingStatus.Cancelled:
                    message = _localizedMessages[MessagesKey.BookingCancelled];
                    break;
                case BookingStatus.Completed:
                    message = _localizedMessages[MessagesKey.BookingCompleted];
                    break;

            }
            await _notificationService.SendNotificationAsync(booking.StudentId, message, NotificationType.Booking);

            return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.BookingUpdatedSuccess]);
        }

        public async Task<dynamic> GetBookingsForTeacherAsync(string teacherId,  BookingStatus status)
        {
            var bookings = await _context.Bookings
                                         .Where(b => b.TeacherId == teacherId && b.Status == status)
                                         .ToListAsync();
            if (bookings == null)
                return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.BookingNotFound]);

            var result = _mapper.Map<List<BookingViewModel>>(bookings);
            return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.DataSuccess],result);

        }

        public async Task<dynamic> GetBookingsForStudentAsync(string studentId)
        {
            var bookings = await _context.Bookings
                                         .Where(b => b.StudentId == studentId)
                                         .ToListAsync();
            if (bookings == null)
                return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.BookingNotFound]);

            var result = _mapper.Map<List<BookingViewModel>>(bookings);
            return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.DataSuccess], result);
        }

    }
}
