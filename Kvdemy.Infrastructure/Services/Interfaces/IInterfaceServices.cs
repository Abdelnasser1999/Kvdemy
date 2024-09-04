using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kvdemy.Web.Services;
using Kvdemy.Infrastructure.Services.Users;
using Kvdemy.Infrastructure.Services.Auth;
using Kvdemy.Infrastructure.Services.Categories;
using Kvdemy.Infrastructure.Services.Cities;
using Kvdemy.Infrastructure.Services.Sliders;
using Kvdemy.Infrastructure.Services.Teachers;
using Kvdemy.Infrastructure.Services.Students;
using Krooti.Infrastructure.Services.Subject;
using Kvdemy.Infrastructure.Services.Subject;
using Kvdemy.Infrastructure.Services.Bookings;
using Kvdemy.Infrastructure.Services.Notifications;
using Kvdemy.Infrastructure.Services.PushNotification;
using Kvdemy.Infrastructure.Services.Reports;
using Kvdemy.Infrastructure.Services.Payments;
using Kvdemy.Infrastructure.Services.Wallet;
using Kvdemy.Infrastructure.Services.Chats;


namespace Kvdemy.Infrastructure.Services.Interfaces
{
    public interface IInterfaceServices
    {

        IFileService fileService { get; }
        IUserService userService { get; }

        IAuthService authService { get; }
		ICategoryService categoryService { get; }
		ISettingsService settingsService { get; }
		ISliderService sliderService { get; }
		ITeacherService teacherService { get; }
		IStudentService studentService { get; }
        ISubjectService subjectService { get; }
        IBookingService bookingService { get; }
        INotificationService notificationService { get; }
        IPushNotificationService pushNotificationService { get; }
        IReportService reportService { get; }
        IPaymentService paymentService { get; }
        IFinanceAccountService financeAccountService { get; }
        IChatService chatService { get; }

    }
}
