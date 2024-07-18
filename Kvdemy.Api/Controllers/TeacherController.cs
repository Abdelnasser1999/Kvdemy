using Kvdemy.Infrastructure.Services.Interfaces;
using Kvdemy.Core.Dtos;
using Kvdemy.Core.Resourses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Kvdemy.Infrastructure.Helpers;
using Kvdemy.Infrastructure.Services.Teachers;
using System.Security.Claims;

namespace Kvdemy.API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TeacherController : BaseController
    {
        private readonly IInterfaceServices _interfaceServices;
        private readonly IStringLocalizer<Messages> _localizedMessages;
        protected string Language;

        public TeacherController(IInterfaceServices interfaceServices, IStringLocalizer<Messages> localizedMessages)
        {
            _interfaceServices = interfaceServices;
            _localizedMessages = localizedMessages;
            Language = Thread.CurrentThread.CurrentUICulture.Name;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterTeacher([FromForm] CreateTeacherDto dto)
        {
            var result = await _interfaceServices.userService.CreateTeacher(dto);
            return Ok(result);
        }

        [HttpPost("{id}/available-hours")]
        public async Task<IActionResult> AddAvailableHours(string id, [FromBody] AvailableHoursModel model)
        {
            var result = await _interfaceServices.teacherService.AddAvailableHoursAsync(id, model);
            return Ok(result);
        }

        [HttpPut("{id}/available-hours")]
        public async Task<IActionResult> UpdateAvailableHours(string id, [FromBody] AvailableHoursModel model)
        {
            var result = await _interfaceServices.teacherService.UpdateAvailableHoursAsync(id, model);
            return Ok(result);
        }

        [HttpPost("{id}/gallery")]
        public async Task<IActionResult> AddGalleryImage(string id, GalleryDto galleryDto)
        {
            var result = await _interfaceServices.teacherService.AddGalleryImageAsync(id, galleryDto);
            return Ok(result);
        }

        [HttpDelete("{id}/gallery/{imageId}")]
        public async Task<IActionResult> DeleteGalleryImage(string id, int imageId)
        {
            var result = await _interfaceServices.teacherService.DeleteGalleryImageAsync(id, imageId);
            return Ok(result);
        }
        [HttpGet("{id}/gallery")]
        public async Task<IActionResult> GetGalleryImages(string id)
        {
            var result = await _interfaceServices.teacherService.GetGalleryImagesAsync(id);
            return Ok(result);
        }
        [HttpPost("{id}/video")]
        public async Task<IActionResult> AddVideo(string id, VideoDto videoDto)
        {
            var result = await _interfaceServices.teacherService.AddVideoAsync(id, videoDto);
            return Ok(result);
        }

        [HttpDelete("{id}/video/{videoId}")]
        public async Task<IActionResult> DeleteVideo(string id, int videoId)
        {
            var result = await _interfaceServices.teacherService.DeleteVideoAsync(id, videoId);
            return Ok(result);
        }

        [HttpGet("{id}/video")]
        public async Task<IActionResult> GetVideos(string id)
        {
            var videos = await _interfaceServices.teacherService.GetVideosAsync(id);
            return Ok(videos);
        }
        [HttpPost("{id}/education")]
        public async Task<IActionResult> AddEducation(string id, EducationDto educationDto)
        {
            var result = await _interfaceServices.teacherService.AddEducationAsync(id, educationDto);
            return Ok(result);
        }

        [HttpDelete("{id}/education/{educationId}")]
        public async Task<IActionResult> DeleteEducation(string id, int educationId)
        {
            var result = await _interfaceServices.teacherService.DeleteEducationAsync(id, educationId);
            return Ok(result);
        }

        [HttpGet("{id}/education")]
        public async Task<IActionResult> GetEducations(string id)
        {
            var educations = await _interfaceServices.teacherService.GetEducationsAsync(id);
            return Ok(educations);
        }
        [HttpPost("{id}/starting-price")]
        public async Task<IActionResult> UpdateStartingPrice(string id, float price)
        {
            var result = await _interfaceServices.teacherService.UpdateStartingPriceAsync(id, price);
            return Ok(result);
        }
        [HttpGet("{id}/starting-price")]
        public async Task<IActionResult> GetStartingPrice(string id)
        {
            var result = await _interfaceServices.teacherService.GetStartingPriceAsync(id);
            return Ok(result);
        }
        [HttpPost("{id}/description")]
        public async Task<IActionResult> AddDescription(string id,string description)
        {
            var result = await _interfaceServices.teacherService.AddDescriptionAsync(id, description);
            return Ok(result);
        }
        [HttpGet("{id}/description")]
        public async Task<IActionResult> GetDescription(string id)
        {
            var result = await _interfaceServices.teacherService.GetDescriptionAsync(id);
            return Ok(result);
        }
        [HttpPost("{id}/generalprofile")]
        public async Task<IActionResult> UpdateProfile(string id, UpdateTeacherGeneralInfoDto profileDto)
        {
            var result = await _interfaceServices.teacherService.UpdateProfileAsync(id, profileDto);
            return Ok(result);
        }

        [HttpGet("{id}/generalprofile")]
        public async Task<IActionResult> GetProfile(string id)
        {
            var result = await _interfaceServices.teacherService.GetProfileAsync(id);
            return Ok(result);
        }
        [HttpPost("{id}/profileImage")]
        public async Task<IActionResult> UpdateProfileImage(string id, ProfileImageDto imageDto)
        {
            var result = await _interfaceServices.teacherService.UpdateProfileImageAsync(id, imageDto);
            return Ok(result);
        }

        [HttpGet("{id}/profileImage")]
        public async Task<IActionResult> GetProfileImage(string id)
        {
            var result = await _interfaceServices.teacherService.GetProfileImageAsync(id);
            return Ok(result);
        }
        [HttpPost("{id}/BookingDetails")]
        public async Task<IActionResult> UpdateBookingDetails(string id , string bookingDetails)
        {
            var result = await _interfaceServices.teacherService.UpdateBookingDetailsAsync(id, bookingDetails);
            return Ok(result);
        }

        [HttpGet("{id}/BookingDetails")]
        public async Task<IActionResult> GetBookingDetails(string id)
        {
            var result = await _interfaceServices.teacherService.GetBookingDetailsAsync(id);
            return Ok(result);
        }

    }
}
