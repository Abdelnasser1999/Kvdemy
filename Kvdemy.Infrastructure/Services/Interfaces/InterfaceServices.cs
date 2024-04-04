using Krooti.Data;
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


namespace Krooti.Infrastructure.Services.Interfaces
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
            userService = new UserService(_mapper, _db, _memoryCache, _httpContextAccessor, _userManager, _env, _localizedMessages, fileService);
        }
        public IFileService fileService { get; private set; }
        public IUserService userService { get; private set; }
    }
}
