//using Krooti.Data;
//using AutoMapper;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.Extensions.Configuration;
//using Microsoft.AspNetCore.Hosting;
//using Krooti.Infrastructure.Services.Categories;
//using Krooti.Infrastructure.Services.Files;
//using ADEK.Infrastructure.Services.Files;
//using Krooti.Infrastructure.Services.Products;
//using Krooti.Infrastructure.Services.Sliders;
//using Krooti.Infrastructure.Services.Auth;
//using Krooti.Infrastructure.Services.Auth;
//using Krooti.Core.Options;
//using Krooti.Core.Resourses;
//using Krooti.Data.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Localization;
//using Microsoft.Extensions.Options;
//using Krooti.Infrastructure.Services.Users;
//using Microsoft.Extensions.Caching.Memory;

//using Krooti.Infrastructure.Services.Cities;
//using Krooti.Infrastructure.Services.Carts;
//using Krooti.Infrastructure.Services.Coupons;
//using Krooti.Infrastructure.Services.Orders;
//using Krooti.Infrastructure.Services.Notifications;


//namespace Krooti.Infrastructure.Services.Interfaces
//{
//    public class InterfaceServices : IInterfaceServices
//    {
//        private readonly IMapper _mapper;
//        private readonly KrootiDbContext _db;
//        private readonly IWebHostEnvironment _env;
//        private readonly UserManager<User> _userManager;
//        private readonly IOptions<JwtOptions> _options;
//        private readonly IStringLocalizer<Messages> _localizedMessages;
//        private readonly SignInManager<User> _signInManager;
//        private RoleManager<IdentityRole> _roleManger;
//        private readonly IHttpContextAccessor _httpContextAccessor;
//        private readonly IMemoryCache _memoryCache;

//        public InterfaceServices(IMapper mapper,
//            KrootiDbContext db,
//            IWebHostEnvironment env,
//            RoleManager<IdentityRole> roleManger,
//            IHttpContextAccessor httpContextAccessor,
//            UserManager<User> userManager,
//               IOptions<JwtOptions> options,
//            IStringLocalizer<Messages> localizedMessages,
//            SignInManager<User> signInManager,
//              IMemoryCache memoryCache)
//        {
//            _mapper = mapper;
//            _db = db;
//            _env = env;
//            _roleManger = roleManger;
//            _httpContextAccessor = httpContextAccessor;
//            _userManager = userManager;
//            _options = options;
//            _localizedMessages = localizedMessages;
//            _signInManager = signInManager;
//            _memoryCache = memoryCache;
//            fileService = new FileService(_env);
          
        
//            categoryService = new CategroyService(_db, _mapper,fileService);
//            productService = new ProductService(_db, _mapper,fileService,categoryService);
//            sliderService = new SliderService(_db, _mapper,fileService);
//            cityService = new CityService(_db, _mapper);
//            settingsService = new SettingsService(_db, _mapper);
  
//            authService = new AuthService(_mapper, _db,_roleManger,_httpContextAccessor,_userManager,_options,_localizedMessages,_signInManager,fileService);
//            userService = new UserService(_mapper, _db,authService,_memoryCache,_httpContextAccessor,_userManager,_env,_localizedMessages, fileService);
//            cartService = new CartService(_db, _mapper, authService);
//            couponService = new CouponService(_db, _mapper, authService);
//            orderService = new OrderService(_db, _mapper, authService);
//            notificationService = new NotificationService(_db, _mapper, authService);
//        }
//        public ICategoryService categoryService { get; private set; }
//        public IFileService fileService { get; private set; }
//        public IProductService productService { get; private set; }
//        public ISliderService sliderService { get; private set; }
//        public IAuthService authService { get; private set; }
//        public IUserService userService { get; private set; }

//        public ICityService cityService { get; private set; }
//        public ISettingsService settingsService { get; private set; }
//        public ICartService cartService { get; private set; }
//        public ICouponService couponService { get; private set; }
//        public IOrderService orderService { get; private set; }
//        public INotificationService notificationService { get; private set; }
//    }
//}
