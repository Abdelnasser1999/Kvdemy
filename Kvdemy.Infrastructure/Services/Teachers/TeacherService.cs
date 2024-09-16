using AutoMapper;
using Kvdemy.Core.Constant;
using Kvdemy.Core.Constants;
using Kvdemy.Core.Dtos;
using Kvdemy.Core.Enums;
using Kvdemy.Core.Exceptions;
using Kvdemy.Core.Resourses;
using Kvdemy.Core.ViewModels;
using Kvdemy.Data.Models;
using Kvdemy.Infrastructure.Helpers;
using Kvdemy.Web.Data;
using Kvdemy.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Kvdemy.Infrastructure.Services.Teachers
{
    public class TeacherService : ITeacherService
    {
        private readonly KvdemyDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IStringLocalizer<Messages> _localizedMessages;

        public TeacherService(KvdemyDbContext dbContext, IMapper mapper
            , IFileService fileService, IStringLocalizer<Messages> localizedMessages)
        {
            _context = dbContext;
            _mapper = mapper;
            _fileService = fileService;
            _localizedMessages = localizedMessages;
        }

        public async Task<dynamic> UpdateAvailableHoursAsync(string userId, AvailableHoursModel model)
		{
			var user = await _context.Users.FindAsync(userId);
			if (user == null || user.UserType != UserType.Teacher)
				return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.ItemNotFound]);

			// تحقق من أن AvailableHours يحتوي على بيانات
			if (model?.AvailableHours == null || !model.AvailableHours.Any())
			{
				return new ApiResponseFailedViewModel("Available hours cannot be empty.");
			}

			// تسلسل AvailableHours إلى JSON وتخزينه في قاعدة البيانات
			user.AvailableHours = JsonConvert.SerializeObject(model);
			_context.Users.Update(user);
			await _context.SaveChangesAsync();
			return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.ItemUpdatedSuccss]);
		}

		public async Task<dynamic> GetAvailableHoursAsync(string userId)
		{
			var user = await _context.Users.FindAsync(userId);
			if (user == null || user.UserType != UserType.Teacher)
				return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.ItemNotFound]);

			try
			{
				// تفكيك JSON إلى Dictionary<string, List<TimeRange>>
				var availableHours = JsonConvert.DeserializeObject<AvailableHoursModel>(user.AvailableHours);
				return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.DataSuccess], availableHours);
			}
			catch (JsonException ex)
			{
				// معالجة الخطأ إذا فشل التحويل
				return new ApiResponseFailedViewModel("Error in parsing available hours data.");
			}
		}
		public async Task<dynamic> AddGalleryImageAsync(string userId, GalleryDto galleryDto)
        {
            if (galleryDto.Image is null)
            {
                throw new InvalidDateException();
            }

            var user = await _context.Users.FindAsync(userId);
            if (user == null || user.UserType != UserType.Teacher) return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.ItemNotFound]);
            var imageUrl = await _fileService.SaveFile(galleryDto.Image, FolderNames.ImagesFolder);

            var galleryImage = new Gallery
            {
                UserId = userId,
                Image = imageUrl
            };

            user.Gallery.Add(galleryImage);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.ItemCreatedSuccss]);
      
        }

        public async Task<dynamic> DeleteGalleryImageAsync(string userId, int imageId)
        {
            var user = await _context.Users.Include(u => u.Gallery)
                                           .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null) return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.ItemNotFound]);

            var galleryImage = user.Gallery.FirstOrDefault(g => g.Id == imageId);

            if (galleryImage == null) return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.ItemDeletedFailed]);

            user.Gallery.Remove(galleryImage);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.ItemDeletedSuccss]);
        }

        public async Task<dynamic> GetGalleryImagesAsync(string userId)
        {
            var user = await _context.Users.Include(u => u.Gallery)
                                           .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null || user.Gallery == null) return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.ItemNotFound]);

            var model = user.Gallery.Select(g => new GalleryViewModel
            {
                Id = g.Id,
                Image = g.Image,
                UserId = g.UserId
            }).ToList();

            return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.DataSuccess] , model);
        }
        public async Task<dynamic> AddVideoAsync(string userId, VideoDto videoDto)
        {
            if (videoDto.Video is null)
            {
                throw new InvalidDateException();
            }

            var user = await _context.Users.FindAsync(userId);
            if (user == null || user.UserType != UserType.Teacher) return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.ItemNotFound]);
            var videoUrl = await _fileService.SaveFile(videoDto.Video, FolderNames.VideosFolder);

            var videoEntry = new Video
            {
                UserId = userId,
                Url = videoUrl
            };

            user.Video.Add(videoEntry);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.ItemCreatedSuccss]);
        }

        public async Task<dynamic> DeleteVideoAsync(string userId, int videoId)
        {
            var user = await _context.Users.Include(u => u.Video)
                                           .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null) return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.ItemNotFound]);

            var videoEntry = user.Video.FirstOrDefault(v => v.Id == videoId);

            if (videoEntry == null) return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.ItemDeletedFailed]);

            user.Video.Remove(videoEntry);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.ItemDeletedSuccss]);
        }

        public async Task<dynamic> GetVideosAsync(string userId)
        {
            var user = await _context.Users.Include(u => u.Video)
                                           .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null || user.Video == null) return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.ItemNotFound]);

            var model = user.Video.Select(v => new VideoViewModel
            {
                Id = v.Id,
                Url = v.Url,
                UserId = v.UserId
            }).ToList();

            return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.DataSuccess], model);
        }

        public async Task<dynamic> AddEducationAsync(string userId, EducationDto educationDto)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null || user.UserType != UserType.Teacher) return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.ItemNotFound]);

            var education = new Education
            {
                UserId = userId,
                InstituteName = educationDto.InstituteName,
                StartDate = educationDto.StartDate,
                EndDate = educationDto.EndDate,
                DegreeTitle = educationDto.DegreeTitle,
                Description = educationDto.Description
            };

            user.Educations.Add(education);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.ItemCreatedSuccss]);
        }

        public async Task<dynamic> DeleteEducationAsync(string userId, int educationId)
        {
            var user = await _context.Users.Include(u => u.Educations)
                                           .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null) return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.ItemNotFound]);

            var education = user.Educations.FirstOrDefault(e => e.Id == educationId);

            if (education == null) return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.ItemDeletedFailed]);

            user.Educations.Remove(education);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.ItemDeletedSuccss]);
        }

        public async Task<dynamic> GetEducationsAsync(string userId)
        {
            var user = await _context.Users.Include(u => u.Educations)
                                           .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null || user.Educations == null) return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.ItemNotFound]);

            var model = user.Educations.Select(e => new EducationViewModel
            {
                Id = e.Id,
                InstituteName = e.InstituteName,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                DegreeTitle = e.DegreeTitle,
                Description = e.Description,
                UserId = e.UserId
            }).ToList();

            return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.DataSuccess], model);
        }

        public async Task<dynamic> UpdateStartingPriceAsync(string userId, float price)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null || user.UserType != UserType.Teacher) return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.ItemNotFound]);

            user.StartingPrice = price;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.ItemUpdatedSuccss]);
        }
        public async Task<dynamic> GetStartingPriceAsync(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null || user.UserType != UserType.Teacher) return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.ItemNotFound]);

            return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.DataSuccess], user.StartingPrice);
        }

        public async Task<dynamic> AddDescriptionAsync(string userId, string description)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null || user.UserType != UserType.Teacher)
                return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.ItemNotFound]);

            user.Description = description;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.ItemUpdatedSuccss]);
        }

        public async Task<dynamic> GetDescriptionAsync(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null || user.UserType != UserType.Teacher)
                return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.ItemNotFound]);

            var description = user.Description;
            return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.DataSuccess], description);
        }

        public async Task<dynamic> UpdateProfileAsync(string userId, UpdateTeacherGeneralInfoDto profileDto)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null || user.UserType != UserType.Teacher)
                return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.ItemNotFound]);

            user.FirstName = profileDto.FirstName;
            user.LastName = profileDto.LastName;
            user.Email = profileDto.Email;
            user.PhoneNumber = profileDto.PhoneNumber;
            user.NameBase = profileDto.NameBase;
            user.Location = profileDto.Location;
            user.DOB = profileDto.DOB;
            user.NationalityId = profileDto.NationalityId;
            user.Gender = profileDto.Gender;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.ItemUpdatedSuccss]);
        }

        public async Task<dynamic> GetProfileAsync(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null || user.UserType != UserType.Teacher)
                return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.ItemNotFound]);

            var profile = new UpdateTeacherGeneralInfoDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                NameBase = user.NameBase,
                Location = user.Location,
                DOB = user.DOB,
                NationalityId = user.NationalityId,
                Gender = user.Gender
            };

            return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.DataSuccess], profile);
        }
        public async Task<dynamic> UpdateProfileImageAsync(string userId, ProfileImageDto imageDto)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null || user.UserType != UserType.Teacher)
                return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.ItemNotFound]);

            var imageUrl = await _fileService.SaveFile(imageDto.ProfileImage, FolderNames.ImagesFolder);
            user.ProfileImage = imageUrl;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.ItemUpdatedSuccss]);
        }
        public async Task<dynamic> GetProfileImageAsync(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null || user.UserType != UserType.Teacher)
                return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.ItemNotFound]);

            return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.DataSuccess], user.ProfileImage);
        }

        public async Task<dynamic> UpdateBookingDetailsAsync(string userId, string bookingDetails)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null || user.UserType != UserType.Teacher)
                return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.ItemNotFound]);

            user.BookingDetails = bookingDetails;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.ItemUpdatedSuccss]);
        }

        public async Task<dynamic> GetBookingDetailsAsync(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null || user.UserType != UserType.Teacher)
                return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.ItemNotFound]);

            return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.DataSuccess], user.BookingDetails);
        }


        public async Task<dynamic> AddSpecializationAsync(string userId, UserSpecialtyDto specializationDto)
        {
            var specialization = new UserSpecialty
            {
                UserId = userId,
                CategoryId = specializationDto.CategoryId,
                SubcategoryId = specializationDto.SubcategoryId
            };
            try
            {
                _context.UserSpecialties.Add(specialization);
                await _context.SaveChangesAsync();
                return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.ItemCreatedSuccss]);

            }catch( Exception ex)
            {
                return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.ItemCreatedFailed]);

            }

        }

        public async Task<dynamic> GetSpecializationsAsync(string userId)
        {
            var specializations = await _context.UserSpecialties
                                                .Where(s => s.UserId == userId)
                                                .Include(s => s.Category)
                                                .Include(s => s.Subcategory)
                                                .ToListAsync();

            var result = specializations.Select(s => new
            {
                s.Id,
                Category = s.Category.Name,
                Subcategory = s.Subcategory.Name
            });

            return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.DataSuccess], result);
        }

        public async Task<dynamic> DeleteSpecializationAsync(int specializationId)
        {
            var specialization = await _context.UserSpecialties.FindAsync(specializationId);

            if (specialization == null)
            {
                return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.ItemNotFound]);
            }

            _context.UserSpecialties.Remove(specialization);
            await _context.SaveChangesAsync();

            return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.ItemDeletedSuccss]);
        }

        public async Task<dynamic> GetTeacherByIdAsync(string teacherId)
        {
            var teacher = await _context.Users
                .Include(u => u.Nationality)
                .Include(u => u.Educations)
                .Include(u => u.Awards)
                .Include(u => u.Gallery)
                .Include(u => u.Video)
                .Include(u => u.UserSpecialties)
                .FirstOrDefaultAsync(u => u.Id == teacherId && u.UserType == UserType.Teacher);

            if (teacher == null)
            {
                return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.ItemNotFound]);
            }

            var teacherProfile = _mapper.Map<TeacherViewModel>(teacher);
            teacherProfile.AvailableHours = JsonConvert.DeserializeObject<dynamic>(teacherProfile.AvailableHours); ;
            return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.DataSuccess], teacherProfile);

        }
        public async Task<FinanceAccountViewModel> GetFinanceAccount(string Id)
        {
            var model = await _context.FinanceAccounts.Include(x => x.User).SingleOrDefaultAsync(x => x.UserId == Id && !x.IsDelete);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }

            var modelViewModel = _mapper.Map<FinanceAccountViewModel>(model);
            return modelViewModel;
        }

        public async Task<List<TransactionsViewModel>> GetTransactions(int Id)
        {
            var model = await _context.AccountTransactions.Where(x => x.FinanceAccountId == Id && !x.IsDelete).ToListAsync();
            if (model == null)
            {
                throw new EntityNotFoundException();
            }

            var modelViewModel = _mapper.Map<List<TransactionsViewModel>>(model);
            return modelViewModel;
        }
        public async Task<dynamic> GetTeacherDashboardStats(string teacherId)
        {
            // ابحث عن المدرس بناءً على teacherId
            var teacher = _context.Users
                .Include(u => u.Gallery) // المعارض
                .Include(u => u.Video) // الفيديوهات
                .Include(u => u.FinanceAccount) // الحساب المالي
                .Include(u => u.UserSpecialties) // التخصصات
                .FirstOrDefault(u => u.Id == teacherId && u.UserType == UserType.Teacher); // المدرس

            if (teacher == null)
            {
                throw new EntityNotFoundException();
            }

            // استعلام عن الحجوزات المتعلقة بالمدرس
            var bookings = _context.Bookings.Where(b => b.TeacherId == teacherId).ToList();

            // حساب الإحصائيات مع استخدام قيم افتراضية في حالة null
            var totalLessons = bookings.Count(); // عدد الدروس الإجمالي
            var totalRevenue = teacher.FinanceAccount?.Balance ?? 0; // الإيرادات
            var averageRating = teacher.Rating ?? 0; // متوسط التقييم
            var totalBookings = bookings.Count(); // عدد الحجوزات الكلي
            var completedBookings = bookings.Count(b => b.Status == BookingStatus.Completed); // الحجوزات المكتملة
            var activeStudents = bookings.Select(b => b.StudentId).Distinct().Count(); // الطلاب النشطين
            var totalMedia = (teacher.Gallery?.Count() ?? 0) + (teacher.Video?.Count() ?? 0); // عدد الموارد التعليمية
            var availableHours = teacher.AvailableHours ?? ""; // الساعات المتاحة (نص فارغ إذا كانت null)

            // التحقق من UserSpecialties وعرض الفئات الأكثر طلباً مع تعيين قائمة فارغة في حال كانت null
            //var mostRequestedSubjects = new List<object>();

            //if (teacher.UserSpecialties != null && teacher.UserSpecialties.Any())
            //{
            //    mostRequestedSubjects = teacher.UserSpecialties
            //        .GroupBy(us => us.Category?.Name ?? "")
            //        .OrderByDescending(g => g.Count())
            //        .Take(3)
            //        .Select(g => new { Subject = g.Key ?? "", Count = g.Count() })
            //        .ToList<object>();
            //}

            // جمع جميع النتائج في متغير واحد مع استخدام القيم الافتراضية
            var dashboardStats = new
            {
                TotalLessons = totalLessons,
                TotalRevenue = totalRevenue,
                AverageRating = averageRating,
                TotalBookings = totalBookings,
                CompletedBookings = completedBookings,
                ActiveStudents = activeStudents,
                TotalMedia = totalMedia,
                AvailableHours = availableHours
            };

            return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.DataSuccess], dashboardStats);
        }
        public async Task<dynamic> AddRatingAsync(string teacherId, double rating, int bookingId)
        {
            // البحث عن المدرس
            var teacher = await _context.Users.FindAsync(teacherId);
            if (teacher == null || teacher.UserType != UserType.Teacher)
            {
                return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.ItemNotFound]);
            }

            // التحقق من صحة التقييم (مثلاً، يجب أن يكون بين 1 و 5)
            if (rating < 1 || rating > 5)
            {
                return new ApiResponseFailedViewModel("Rating must be between 1 and 5.");
            }

            // البحث عن الحجز
            var booking = await _context.Bookings.FindAsync(bookingId);
            if (booking == null || booking.TeacherId != teacherId)
            {
                return new ApiResponseFailedViewModel("Booking not found or does not belong to the specified teacher.");
            }

            // التحقق من أن الحجز لم يتم تقييمه من قبل
            if (booking.IsRated)
            {
                return new ApiResponseFailedViewModel("This booking has already been rated.");
            }

            // تحديث إجمالي التقييمات وعدد التقييمات
            teacher.RatingSum =(int) ((teacher.RatingSum ?? 0) + rating);
            teacher.RatingCount = (teacher.RatingCount ?? 0) + 1;

            // حساب التقييم المتوسط
            teacher.Rating = (float)(teacher.RatingSum / teacher.RatingCount);

            // تحديث بيانات المدرس في قاعدة البيانات
            _context.Users.Update(teacher);

            // تحديث حالة الحجز إلى مقيم
            booking.IsRated = true;
            _context.Bookings.Update(booking);

            // حفظ التغييرات في قاعدة البيانات
            await _context.SaveChangesAsync();

            return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.ItemUpdatedSuccss]);
        }


    }
}
