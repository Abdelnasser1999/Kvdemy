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

        public InterfaceServices(IMapper mapper,
            KvdemyDbContext db,
            IWebHostEnvironment env,
            RoleManager<IdentityRole> roleManger,
            IHttpContextAccessor httpContextAccessor,
            UserManager<User> userManager,
               IOptions<JwtOptions> options,
            IStringLocalizer<Messages> localizedMessages,
            SignInManager<User> signInManager,
              IMemoryCache memoryCache)
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
            fileService = new FileService(_env);
            userService = new UserService(_mapper, _db, authService, _memoryCache, _httpContextAccessor, _userManager, _env, _localizedMessages, fileService);
            authService = new AuthService(_mapper, _db, _roleManger, _httpContextAccessor, _userManager, _options, _localizedMessages, _signInManager, fileService);
			categoryService = new CategroyService(_db, _mapper, fileService);
			settingsService = new SettingsService(_db, _mapper);
			sliderService = new SliderService(_db, _mapper, fileService);
            teacherService = new TeacherService(_db, _mapper, fileService, _localizedMessages);
            studentService = new StudentService(_db, _mapper, fileService, _localizedMessages);

		}
		public IFileService fileService { get; private set; }
        public IUserService userService { get; private set; }
        public IAuthService authService { get; private set; }
		public ICategoryService categoryService { get; private set; }
		public ISettingsService settingsService { get; private set; }
		public ISliderService sliderService { get; private set; }
        public ITeacherService teacherService { get; private set; }
        public IStudentService studentService { get; private set; }

    }
}
