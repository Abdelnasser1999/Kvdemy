using Kvdemy.Data;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Caching.Memory;
using Kvdemy.Web.Data;
using Kvdemy.Data.Models;
using Kvdemy.Web.Services;
using Kvdemy.Core.Options;
using Kvdemy.Core.Resourses;
using Kvdemy.Infrastructure.Services.Users;
using Kvdemy.Infrastructure.Services.Auth;
using Kvdemy.Infrastructure.Services.Categories;
using Kvdemy.Infrastructure.Services.Cities;
using Krooti.Infrastructure.Services.Cities;
using Kvdemy.Infrastructure.Services.Sliders;
using Kvdemy.Infrastructure.Services.Teachers;
using Kvdemy.Infrastructure.Services.Students;
using Krooti.Infrastructure.Services.Subject;
using Kvdemy.Infrastructure.Services.Subject;
using Kvdemy.Infrastructure.Services.Bookings;
using Krooti.Infrastructure.Services.Bookings;
using Kvdemy.Infrastructure.Services.Notifications;
using Krooti.Infrastructure.Services.Notifications;
using Kvdemy.Infrastructure.Services.PushNotification;
using FirebaseAdmin.Messaging;
using Krooti.Infrastructure.Services.PushNotification;
using Kvdemy.Infrastructure.Services.Reports;
using Kvdemy.Infrastructure.Services.Payments;
using Krooti.Infrastructure.Services.Payments;
using Kvdemy.Infrastructure.Services.Wallet;
using Kvdemy.Infrastructure.Services.Chats;
using Krooti.Infrastructure.Services.Chats;


namespace Kvdemy.Infrastructure.Services.Interfaces
{
    public class InterfaceServices : IInterfaceServices
    {
        private readonly IMapper _mapper;
        private readonly KvdemyDbContext _db;
        private readonly IWebHostEnvironment _env;
        private readonly UserManager<User> _userManager;
        private readonly IOptions<JwtOptions> _options;
        private readonly IStringLocalizer<Messages> _localizedMessages;
        private readonly SignInManager<User> _signInManager;
        private RoleManager<IdentityRole> _roleManger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMemoryCache _memoryCache;
        private readonly FirebaseMessaging _firebaseMessaging;

        public InterfaceServices(IMapper mapper,
            KvdemyDbContext db,
            IWebHostEnvironment env,
            RoleManager<IdentityRole> roleManger,
            IHttpContextAccessor httpContextAccessor,
            UserManager<User> userManager,
               IOptions<JwtOptions> options,
            IStringLocalizer<Messages> localizedMessages,
            SignInManager<User> signInManager,
              IMemoryCache memoryCache,
              FirebaseMessaging firebaseMessaging)
        {
            _mapper = mapper;
            _db = db;
            _env = env;
            _roleManger = roleManger;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _options = options;
            _localizedMessages = localizedMessages;
            _signInManager = signInManager;
            _memoryCache = memoryCache;
            _firebaseMessaging = firebaseMessaging;
            fileService = new FileService(_env);
            userService = new UserService(_mapper, _db, authService, _memoryCache, _httpContextAccessor, _userManager, _env, _localizedMessages, fileService, _signInManager);
            authService = new AuthService(_mapper, _db, _roleManger, _httpContextAccessor, _userManager, _options, _localizedMessages, _signInManager, fileService);
			categoryService = new CategroyService(_db, _mapper, fileService);
			settingsService = new SettingsService(_db, _mapper);
			sliderService = new SliderService(_db, _mapper, fileService);
            teacherService = new TeacherService(_db, _mapper, fileService, _localizedMessages);
            studentService = new StudentService(_db, _mapper, fileService, _localizedMessages);
            subjectService = new SubjectService(_db, _mapper, fileService, _localizedMessages);
            pushNotificationService = new PushNotificationService(_db, _mapper, fileService, _localizedMessages, _firebaseMessaging);
            notificationService = new NotificationService(_db, _mapper, pushNotificationService, _localizedMessages);
            paymentService = new PaymentService(_db, _mapper, notificationService, _localizedMessages);
            bookingService = new BookingService(_db, _mapper, notificationService, _localizedMessages,paymentService);
			reportService = new ReportService(_db, _mapper, _localizedMessages);
            financeAccountService = new FinanceAccountService(_db, _mapper, authService, _localizedMessages);
            chatService = new ChatService(_db, _mapper, notificationService, _localizedMessages,fileService);

        }
        public IFileService fileService { get; private set; }
        public IUserService userService { get; private set; }
        public IAuthService authService { get; private set; }
		public ICategoryService categoryService { get; private set; }
		public ISettingsService settingsService { get; private set; }
		public ISliderService sliderService { get; private set; }
        public ITeacherService teacherService { get; private set; }
        public IStudentService studentService { get; private set; }
        public ISubjectService subjectService { get; private set; }
        public IBookingService bookingService { get; private set; }
        public INotificationService notificationService { get; private set; }
        public IPushNotificationService  pushNotificationService{ get; private set; }
        public IReportService  reportService{ get; private set; }
        public IPaymentService  paymentService{ get; private set; }
        public IFinanceAccountService financeAccountService { get; private set; }
        public IChatService chatService { get; private set; }

    }
}
