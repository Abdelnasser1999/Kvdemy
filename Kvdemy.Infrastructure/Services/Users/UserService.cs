using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http.HttpResults;
using Kvdemy.Web.Data;
using Kvdemy.Web.Services;
using Kvdemy.Data.Models;
using Kvdemy.Core.Resourses;
using Kvdemy.Core.ViewModels;
using Kvdemy.Core.Constant;
using Kvdemy.Core.Dtos.Helpers;
using Kvdemy.Core.Dtos;
using Kvdemy.Core.Exceptions;
using Kvdemy.Core.Constants;
using Kvdemy.Infrastructure.Services.Auth;
using Kvdemy.Core.Enums;
using Azure;




namespace Kvdemy.Infrastructure.Services.Users
{
    public class UserService : IUserService
    {
        private readonly KvdemyDbContext _db;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStringLocalizer<Messages> _localizedMessages;
   
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMemoryCache _memoryCache;

        private readonly IAuthService _authService;

        private readonly IFileService _fileService;

        public UserService(
             IMapper mapper,
             KvdemyDbContext db,
            IAuthService authService,
            IMemoryCache memoryCache,
            IHttpContextAccessor httpContextAccessor,
            UserManager<User> userManager,
            IWebHostEnvironment webHostEnvironment,
            IStringLocalizer<Messages> localizedMessages,
            IFileService fileService
            )
        {
            _db = db;
            _mapper = mapper;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
            _localizedMessages = localizedMessages;

            _httpContextAccessor = httpContextAccessor;
            _memoryCache = memoryCache;
            _authService = authService;
            //_interfaceServices = interfaceServices;
            _fileService = fileService;
        }

        public async Task<dynamic> CreateStudent(CreateStudentDto dto)
        {
            var user = _mapper.Map<User>(dto);
            Random random = new Random();
            var generatedOtp   = random.Next(1000, 10000);
            user.OtpCode = generatedOtp.ToString();

          
        
            user.UserName = dto.Email;
            user.UserType = UserType.Student;
            if (dto.Email != null) 
            {
                bool emailIsExist = await _db.Users.AnyAsync(x => x.Email == dto.Email);
                if (emailIsExist)
                {
                        return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.DuplicateEmail]);
                }
            }

            

            bool phoneIsExist = await _db.Users.AnyAsync(x => x.PhoneNumber == dto.PhoneNumber);
            if (phoneIsExist)
            {
                return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.DuplicatePhone]);
            }


            if (dto.ProfileImage != null)
            {
                user.ProfileImage = await _fileService.SaveFile(dto.ProfileImage, FolderNames.ImagesFolder);
            }

            var NationalityExist = await _db.Nationalities.AnyAsync(x=>x.Id == dto.NationalityId);
            if (!NationalityExist)
            {
                throw new NationalityNotFoundException();
            }

            try
            {
                var result =   _userManager.CreateAsync(user, dto.Password).GetAwaiter().GetResult();
				if (result.Succeeded)
                {
                 
                    return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.OtpSuccess], user.OtpCode);
				}
                else
                {

                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    var resultMsg = $"User creation failed. Errors: {errors}";
                    return new ApiResponseFailedViewModel(resultMsg);


                }
            }
            catch (DbUpdateException ex)
            {
                // Check if the exception is related to a unique constraint violation
                if (ex.InnerException is SqlException sqlException && sqlException.Number == 2601)
                {
                    // Handle the duplicate key violation for the 'Email' column
                    return new ApiResponseFailedViewModel( "Email address is already in use. Please choose a different email.");
                }
                else
                {
                    // Handle other types of DbUpdateException or rethrow the exception
                    throw;
                }
            }
          
            
        }
        public async Task<dynamic> CreateTeacher(CreateTeacherDto dto)
        {
            var user = _mapper.Map<User>(dto);
            Random random = new Random();
            var generatedOtp   = random.Next(1000, 10000);
            user.OtpCode = generatedOtp.ToString();

          
        
            user.UserName = dto.Email;
            user.UserType = UserType.Teacher;
            if (dto.Email != null) 
            {
                bool emailIsExist = await _db.Users.AnyAsync(x => x.Email == dto.Email);
                if (emailIsExist)
                {
                    throw new DuplicateEmailException();
                }
            }

            

            bool phoneIsExist = await _db.Users.AnyAsync(x => x.PhoneNumber == dto.PhoneNumber);
            if (phoneIsExist)
            {
                throw new DuplicatePhoneException();
            }


            if (dto.ProfileImage != null)
            {
                user.ProfileImage = await _fileService.SaveFile(dto.ProfileImage, FolderNames.ImagesFolder);
            }

            var NationalityExist = await _db.Nationalities.AnyAsync(x=>x.Id == dto.NationalityId);
            if (!NationalityExist)
            {
                throw new NationalityNotFoundException();
            }

            try
            {
                var result =   _userManager.CreateAsync(user, dto.Password).GetAwaiter().GetResult();
				if (result.Succeeded)
                {
                    var Teacher = _mapper.Map<TeacherViewModel>(user);
                    return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.ItemCreatedSuccss], Teacher);
                }
                else
                {

                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    var resultMsg = $"User creation failed. Errors: {errors}";
                    return new ApiResponseFailedViewModel(resultMsg);


                }
            }
            catch (DbUpdateException ex)
            {
                // Check if the exception is related to a unique constraint violation
                if (ex.InnerException is SqlException sqlException && sqlException.Number == 2601)
                {
                    // Handle the duplicate key violation for the 'Email' column
                    return new ApiResponseFailedViewModel( "Email address is already in use. Please choose a different email.");
                }
                else
                {
                    // Handle other types of DbUpdateException or rethrow the exception
                    throw;
                }
            }
          
            
        }
        public async Task<RegisterHelperViewModel> GetRegisterHelper()
        {
            var response = new RegisterHelperViewModel();
            ////Nationalities
            var queryNationalities = _db.Nationalities.Where(x => !x.IsDelete);
            if (queryNationalities == null)
            {
                throw new EntityNotFoundException();
            }
            var nationalitiesList = await queryNationalities.ToListAsync();
            response.nationalities = _mapper.Map<List<NationalityViewModel>>(nationalitiesList);
            ////Languages
            var queryLanguages = _db.Languages.Where(x => !x.IsDelete);
            if (queryLanguages == null)
            {
                throw new EntityNotFoundException();
            }
            var LanguagesList = await queryLanguages.ToListAsync();
            response.languages = _mapper.Map<List<LanguageViewModel>>(LanguagesList);
            ////LanguageLevels
            var queryLevels = _db.LanguageLevels.Where(x => !x.IsDelete);
            if (queryLevels == null)
            {
                throw new EntityNotFoundException();
            }
            var LevelsList = await queryLevels.ToListAsync();
            response.levels = _mapper.Map<List<LanguageLevelViewModel>>(LevelsList);
            return response;
        }
        public async Task<List<NationalityViewModel>> GetNationalities()
        {
            ////Nationalities
            var queryNationalities = _db.Nationalities.Where(x => !x.IsDelete);
            if (queryNationalities == null)
            {
                throw new EntityNotFoundException();
            }
            var nationalitiesList = await queryNationalities.ToListAsync();
            var nationalities = _mapper.Map<List<NationalityViewModel>>(nationalitiesList);
            return nationalities;
        }
        public async Task<PaginationWebViewModel> GetAllStudents(Pagination pagination, Query query)
        {

            var queryString = _db.Users.Where(x => !x.IsDelete && x.UserType == UserType.Student && (x.FirstName.Contains(query.GeneralSearch) ||x.LastName.Contains(query.GeneralSearch) || x.PhoneNumber.Contains(query.GeneralSearch) || x.Email.Contains(query.GeneralSearch) || string.IsNullOrWhiteSpace(query.GeneralSearch))).AsQueryable();

            var dataCount = queryString.Count();
            pagination.Total = dataCount;
            var skipValue = pagination.GetSkipValue();
            var dataList = await queryString.Skip(skipValue).Take(pagination.PerPage).ToListAsync();
            var users = _mapper.Map<List<StudentViewModel>>(dataList);
            var pages = pagination.GetPages(dataCount);

            var result = new PaginationWebViewModel
            {
                data = users,
                meta = new Meta
                {
                    page = pagination.Page,
                    perpage = pagination.PerPage,
                    pages = pages,
                    total = dataCount
                }
            };

            return result;
        }
		public async Task<StudentViewModel> GetStudent(string Id)
		{
			var model = await _db.Users.Include(x => x.Nationality).SingleOrDefaultAsync(x => x.Id == Id && !x.IsDelete);
			if (model == null)
			{
				throw new EntityNotFoundException();
			}

			var modelViewModel = _mapper.Map<StudentViewModel>(model);
			return modelViewModel;
		}
        public async Task<PaginationWebViewModel> GetAllTeachers(Pagination pagination, Query query)
        {

            var queryString = _db.Users.Where(x => !x.IsDelete && x.UserType == UserType.Teacher && (x.FirstName.Contains(query.GeneralSearch) ||x.LastName.Contains(query.GeneralSearch) || x.PhoneNumber.Contains(query.GeneralSearch) || x.Email.Contains(query.GeneralSearch) || string.IsNullOrWhiteSpace(query.GeneralSearch))).AsQueryable();

            var dataCount = queryString.Count();
            pagination.Total = dataCount;
            var skipValue = pagination.GetSkipValue();
            var dataList = await queryString.Skip(skipValue).Take(pagination.PerPage).ToListAsync();
            var users = _mapper.Map<List<TeacherViewModel>>(dataList);
            var pages = pagination.GetPages(dataCount);

            var result = new PaginationWebViewModel
            {
                data = users,
                meta = new Meta
                {
                    page = pagination.Page,
                    perpage = pagination.PerPage,
                    pages = pages,
                    total = dataCount
                }
            };

            return result;
        }
		public async Task<TeacherViewModel> GetTeacher(string Id)
		{
			var model = await _db.Users.Include(x => x.Nationality).SingleOrDefaultAsync(x => x.Id == Id && !x.IsDelete);
			if (model == null)
			{
				throw new EntityNotFoundException();
			}

			var modelViewModel = _mapper.Map<TeacherViewModel>(model);
			return modelViewModel;
		}

        public async Task<dynamic> Delete(string id)
        {
            var user = _db.Users.SingleOrDefault(x => x.Id == id);
            if (user == null)
            {
                return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.UserNotFound]);
            }
            else
            {

                _db.Users.Remove(user);
                _db.SaveChanges();
                return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.UserDeletedSuccss], user.Id);

            }



        }


        //public async Task<PagingAPIViewModel> GetStudent(string searchKey, int page)
        //{
        //    var pageSize = 10;
        //    var totalmodels = _db.Users.Count(x => !x.IsDelete && x.FirstName.Contains(searchKey) || x.LastName.Contains(searchKey)|| x.Email.Contains(searchKey) || x.PhoneNumber.Contains(searchKey) || string.IsNullOrWhiteSpace(searchKey));
        //    var totalPages = (int)Math.Ceiling(totalmodels / (double)pageSize);
        //    if (totalPages != 0)
        //    {
        //        // Ensure page number is within valid range
        //        page = Math.Clamp(page, 1, totalPages);
        //    }
        //    var skipCount = (page - 1) * pageSize;

        //    IQueryable<User> query = _db.Users

        //        .Where(x => !x.IsDelete && x.FirstName.Contains(searchKey) || x.LastName.Contains(searchKey) || x.Email.Contains(searchKey) || x.PhoneNumber.Contains(searchKey) || string.IsNullOrWhiteSpace(searchKey))
        //        .OrderByDescending(x => x.CreatedAt)
        //        .Skip(skipCount)
        //        .Take(pageSize);
        //    var modelquery = await query.ToListAsync();
        //    var modelViewModels = _mapper.Map<List<StudentViewModel>>(modelquery);

        //    var pagingResult = new PagingAPIViewModel
        //    {
        //        Data = modelViewModels,
        //        NumberOfPages = totalPages,
        //        CureentPage = page
        //    };

        //    return pagingResult;
        //}

        //public async Task<dynamic> UserExist(string searchKey)
        //{
        //    if (!searchKey.IsNullOrEmpty())
        //    {
        //        var IsExist = await _db.Users.AnyAsync(x => x.Email.Contains(searchKey) || x.PhoneNumber.Contains(searchKey) );
        //        if (IsExist)
        //        {
        //            return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.DataSuccess], true);
        //        }
        //        else
        //        {
        //            return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.DataSuccess], false);
        //        }
        //    }
        //    else
        //    {
        //        return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.DataSuccess], false);
        //    }



        //}

        //public async Task<PaginationWebViewModel> GetAll(Pagination pagination, Query query)
        //{

        //    var queryString = _db.Users.Where(x => !x.IsDelete && x.UserType==UserType.Client && (x.Name.Contains(query.GeneralSearch) || x.PhoneNumber.Contains(query.GeneralSearch) || x.Email.Contains(query.GeneralSearch) || string.IsNullOrWhiteSpace(query.GeneralSearch))).AsQueryable();

        //    var dataCount = queryString.Count();
        //    pagination.Total = dataCount;
        //    var skipValue = pagination.GetSkipValue();
        //    var dataList = await queryString.Skip(skipValue).Take(pagination.PerPage).ToListAsync();
        //    var users = _mapper.Map<List<UserViewModel>>(dataList);
        //    var pages = pagination.GetPages(dataCount);

        //    var result = new PaginationWebViewModel
        //    {
        //        data = users,
        //        meta = new Meta
        //        {
        //            page = pagination.Page,
        //            perpage = pagination.PerPage,
        //            pages = pages,
        //            total = dataCount
        //        }
        //    };

        //    return result;
        //}





        //public async Task<dynamic> Update(UpdateUserDto dto)
        //{

        //    var user = _db.Users.SingleOrDefault(x => x.Id == dto.Id);

        //    if (user == null)
        //    {
        //        return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.UserNotFound]);
        //    }else {

        //        string img = null;

        //        if (dto.Name == null)
        //        {
        //            dto.Name = user.Name;
        //        }

        //        if (dto.Email == null)
        //        {
        //            dto.Email = user.Email;
        //        }


        //        if (dto.CityId == null)
        //        {
        //            dto.CityId = user.CityId;
        //        }




        //        if (dto.Gender == null)
        //        {
        //            dto.Gender = user.Gender;
        //        }

        //        if (dto.PhoneNumber == null)
        //        {
        //            dto.PhoneNumber = user.PhoneNumber;
        //        }



        //        if (dto.FCMToken == null)
        //        {
        //            dto.FCMToken = user.FCMToken;
        //        }


        //        if (dto.Status == null)
        //        {
        //            dto.Status = user.Status;
        //        }


        //        if (dto.ProfileImage == null)
        //        {
        //            img = user.ProfileImage;
        //        }




        //        var updatedUser = _mapper.Map(dto, user);


        //        if (dto.ProfileImage != null)
        //        {
        //            user.ProfileImage = await _fileService.SaveFile(dto.ProfileImage, FolderNames.ImagesFolder);

        //        }else
        //        {
        //            updatedUser.ProfileImage = img;
        //        }
        //        _db.Users.Update(updatedUser);
        //        int affectedRow =   _db.SaveChanges();

        //        if(affectedRow > 0)
        //        {

        //            var userVm = _mapper.Map<UserViewModel>(updatedUser);

        //            return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.UserUpdatedSuccss], userVm);
        //        }else
        //        {
        //            return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.UserUpdatedFailed]);
        //        }


        //    }
        //}
        //public async Task<dynamic> Update(UpdateUserDto dto, string language)
        //{

        //    var user = _db.Users.SingleOrDefault(x => x.Id == dto.Id);

        //    if (user == null)
        //    {
        //        return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.UserNotFound]);
        //    }
        //    else
        //    {

        //        string img = null;

        //        if (dto.Name == null)
        //        {
        //            dto.Name = user.Name;
        //        }

        //        if (dto.Email == null)
        //        {
        //            dto.Email = user.Email;
        //        }

        //        //if (dto.PhoneNumber == null)
        //        //{
        //        //    dto.PhoneNumber = user.PhoneNumber;
        //        //}



        //        if (dto.FCMToken == null)
        //        {
        //            dto.FCMToken = user.FCMToken;
        //        }


        //        if (dto.Status == null)
        //        {
        //            dto.Status = user.Status;
        //        }


        //        if (dto.ProfileImage == null)
        //        {
        //            img = user.ProfileImage;
        //        }




        //        var updatedUser = _mapper.Map(dto, user);


        //        if (dto.ProfileImage != null)
        //        {
        //            user.ProfileImage = await _fileService.SaveFile(dto.ProfileImage, FolderNames.ImagesFolder);

        //        }
        //        else
        //        {
        //            updatedUser.ProfileImage = img;
        //        }
        //        _db.Users.Update(updatedUser);
        //        int affectedRow = _db.SaveChanges();

        //        if (affectedRow > 0)
        //        {

        //            var userVm = _mapper.Map<UserViewModel>(updatedUser);

        //            return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.UserUpdatedSuccss], userVm);
        //        }
        //        else
        //        {
        //            return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.UserUpdatedFailed]);
        //        }


        //    }
        //}




        //public async Task<UserViewModel> Get(string Id)
        //{
        //    var model = await _db.Users.Include(x=>x.City).SingleOrDefaultAsync(x => x.Id == Id && !x.IsDelete);
        //    if (model == null)
        //    {
        //        throw new EntityNotFoundException();
        //    }

        //    var modelViewModel = _mapper.Map<UserViewModel>(model);
        //    return modelViewModel;
        //}



    }
}
