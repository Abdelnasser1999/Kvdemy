﻿using AutoMapper;
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
using Newtonsoft.Json;
using Kvdemy.Infrastructure.Helpers;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using System.Net.Mail;
using System.Net;




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
        private readonly SignInManager<User> _signInManager;
        private readonly string accessKeyId = "AKIA2UC3EQAXVONVLYVO";
        private readonly string secretAccessKey = "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";
        private readonly AmazonSimpleEmailServiceClient client;

        public UserService(
             IMapper mapper,
             KvdemyDbContext db,
            IAuthService authService,
            IMemoryCache memoryCache,
            IHttpContextAccessor httpContextAccessor,
            UserManager<User> userManager,
            IWebHostEnvironment webHostEnvironment,
            IStringLocalizer<Messages> localizedMessages,
            IFileService fileService,
            SignInManager<User> signInManager
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
            _signInManager = signInManager;
            client = new AmazonSimpleEmailServiceClient(accessKeyId, secretAccessKey, Amazon.RegionEndpoint.USEast1);

        }
        public async Task<bool> AdminLoginAsync(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, false, false);
            return result.Succeeded;
        }

        public async Task<bool> SendVerificationCode(string emailAddress, string verificationCode)
        {
            try
            {
                //var client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
                //{
                //    Credentials = new NetworkCredential("829645233e884e", "63146993323d54"),
                //    EnableSsl = true
                //};
                //client.Send("info@kvdemy.com", "abd.alnasser.m.alaraj@gmail.com", "KVDEMY Verification Code", $"Your KVDEMY verification code is: {verificationCode}");

                var smtpClient = new SmtpClient("smtp.titan.email")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("no-reply@kvdemy.com", "_jcKhWs?NL_/D3C"),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("no-reply@kvdemy.com"),
                    Subject = "KVDEMY Verification Code",
                    Body = $"Your KVDEMY verification code is: {verificationCode}",
                    IsBodyHtml = false,
                };

                mailMessage.To.Add(emailAddress);

                await smtpClient.SendMailAsync(mailMessage);

                Console.WriteLine("Verification email sent successfully.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send email via SMTP: {ex.Message}");
                return false;
            }
        }



        public async Task<dynamic> CreateStudent(CreateStudentDto dto)
        {
            var user = _mapper.Map<User>(dto);
            Random random = new Random();
            var generatedOtp   = random.Next(1000, 10000);
            user.OtpCode = generatedOtp.ToString();



            user.UserName = dto.Email;
            user.UserType = UserType.Student;
            user.Status = UserStatus.active;
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
            if (!await SendVerificationCode(dto.Email, user.OtpCode))
            {
                return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.FailedSendOtp]);
            }
            try
            {
                var result =   _userManager.CreateAsync(user, dto.Password).GetAwaiter().GetResult();
				if (result.Succeeded)
                {
                 
                    return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.ItemCreatedSuccss], user.OtpCode);
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
        public async Task<dynamic> CreateTeacher(CreateTeacherDto dto )
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
            AvailableHoursModel model = new AvailableHoursModel();
            model.AvailableHours = new Dictionary<string, List<TimeRange>>{
                { "Saturday", new List<TimeRange> { new TimeRange { From = "00:00 AM", To = "00:00 PM" } } },
                { "Sunday", new List<TimeRange> { new TimeRange { From = "00:00 AM", To = "00:00 PM" } } },
                { "Monday", new List<TimeRange> { new TimeRange { From = "00:00 AM", To = "00:00 PM" } } },
                { "Tuesday", new List<TimeRange> { new TimeRange { From = "00:00 AM", To = "00:00 PM" } } },
                { "Wednesday", new List<TimeRange> { new TimeRange { From = "00:00 AM", To = "00:00 PM" } } },
                { "Thursday", new List<TimeRange> { new TimeRange { From = "00:00 AM", To = "00:00 PM" } } },
                { "Friday", new List<TimeRange> { new TimeRange { From = "00:00 AM", To = "00:00 PM" } } },
            };
            user.AvailableHours = JsonConvert.SerializeObject(model);
            //user.StartingPrice = 10;
            user.CreatedAt = DateTime.UtcNow;
            user.Status = UserStatus.inActive;
            if (!await SendVerificationCode(dto.Email, user.OtpCode))
            {
                return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.FailedSendOtp]);
            }

            try
            {
                var result =   _userManager.CreateAsync(user, dto.Password).GetAwaiter().GetResult();
				if (result.Succeeded)
                {
                    var financeAccount = new FinanceAccount();
                    financeAccount.UserId = user.Id;
                    financeAccount.Balance = 0;

                    await _db.FinanceAccounts.AddAsync(financeAccount);
                    await _db.SaveChangesAsync();
                    return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.ItemCreatedSuccss], user.OtpCode);
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
        public async Task<PaginationWebViewModel> GetNewTeachers(Pagination pagination, Query query)
        {

            var queryString = _db.Users.Where(x => !x.IsDelete && (x.Status == UserStatus.inActive || x.Status == UserStatus.Rejected) && x.UserType == UserType.Teacher && (x.FirstName.Contains(query.GeneralSearch) ||x.LastName.Contains(query.GeneralSearch) || x.PhoneNumber.Contains(query.GeneralSearch) || x.Email.Contains(query.GeneralSearch) || string.IsNullOrWhiteSpace(query.GeneralSearch))).AsQueryable();

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
		public async Task<dynamic> GetProfile(string Id)
        {
			var user = await _db.Users.Include(x => x.Nationality).Include(x => x.FinanceAccount).SingleOrDefaultAsync(x => x.Id == Id && !x.IsDelete);
			if (user == null)
			{
				throw new EntityNotFoundException();
			}
            var model = _mapper.Map<UserViewModel>(user);

            // Attempt to deserialize the AdditionalInformation string to a dynamic object
            if (!string.IsNullOrEmpty(model.AdditionalInformation))
            {
                try
                {
                    model.AdditionalInformation = JsonConvert.DeserializeObject<dynamic>(model.AdditionalInformation);
                }
                catch (JsonReaderException ex)
                {
                    // Log the exception and handle the invalid JSON case
                    // For example, you can log the error and set additionalInfo to null
                    Console.WriteLine($"Error deserializing AdditionalInformation: {ex.Message}");
                }
            }

            return model;
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
        public async Task<dynamic> AcceptTeacher(string id)
        {
            var user = _db.Users.SingleOrDefault(x => x.Id == id);
            if (user == null)
            {
                return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.UserNotFound]);
            }
            else
            {
                user.Status = UserStatus.active;
                _db.Users.Update(user);
                await _db.SaveChangesAsync();
                return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.UserUpdatedSuccss], user.Id);
            }
        }

        public async Task<dynamic> RejectTeacher(string id)
        {
            var user = _db.Users.SingleOrDefault(x => x.Id == id);
            if (user == null)
            {
                return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.UserNotFound]);
            }
            else
            {
                user.Status = UserStatus.Rejected;
                _db.Users.Update(user);
                await _db.SaveChangesAsync();
                return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.UserUpdatedSuccss], user.Id);
            }
        }

        public async Task<dynamic> DeleteAccount(string id)
        {
            var user = await _db.Users.Include(u => u.FinanceAccount).SingleOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.UserNotFound]);
            }

            user.Email = "deleted";
            user.PhoneNumber = "deleted";
            user.ProfileImage = "deleted";
            user.NormalizedEmail = "deleted";
            user.UserName = "deleted";
            user.NormalizedUserName = "deleted";
            user.IsDelete = true;
            await _db.SaveChangesAsync();

            return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.UserDeletedSuccss], user.Id);
        }

    }
}
