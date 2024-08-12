using Kvdemy.Core.Dtos;
using Kvdemy.Core.ViewModels;
using Kvdemy.Infrastructure.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Infrastructure.Services.Teachers
{
    public interface ITeacherService
    {
        Task<dynamic> GetTeacherByIdAsync(string teacherId);

        Task<dynamic> UpdateAvailableHoursAsync(string userId, AvailableHoursModel model);
        Task<dynamic> GetAvailableHoursAsync(string userId);
        Task<dynamic> AddGalleryImageAsync(string userId, GalleryDto galleryDto);
        Task<dynamic> DeleteGalleryImageAsync(string userId, int imageId);
        Task<dynamic> GetGalleryImagesAsync(string userId);
        Task<dynamic> AddVideoAsync(string userId, VideoDto videoDto);
        Task<dynamic> DeleteVideoAsync(string userId, int videoId);
        Task<dynamic> GetVideosAsync(string userId);
        Task<dynamic> AddEducationAsync(string userId, EducationDto educationDto);
        Task<dynamic> DeleteEducationAsync(string userId, int educationId);
        Task<dynamic> GetEducationsAsync(string userId);
        Task<dynamic> UpdateStartingPriceAsync(string userId, float price);
        Task<dynamic> GetStartingPriceAsync(string userId);
        Task<dynamic> AddDescriptionAsync(string userId, string description);
        Task<dynamic> GetDescriptionAsync(string userId);
        Task<dynamic> UpdateProfileAsync(string userId, UpdateTeacherGeneralInfoDto profileDto);
        Task<dynamic> GetProfileAsync(string userId);
        Task<dynamic> UpdateProfileImageAsync(string userId, ProfileImageDto imageDto);
        Task<dynamic> GetProfileImageAsync(string userId);
        Task<dynamic> UpdateBookingDetailsAsync(string userId, string bookingDetails);
        Task<dynamic> GetBookingDetailsAsync(string userId);

        Task<dynamic> AddSpecializationAsync(string userId, UserSpecialtyDto specializationDto);
        Task<dynamic> GetSpecializationsAsync(string userId);
        Task<dynamic> DeleteSpecializationAsync(int specializationId);

    }
}
