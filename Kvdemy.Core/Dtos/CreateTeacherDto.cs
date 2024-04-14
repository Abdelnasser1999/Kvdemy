using Kvdemy.Core.Constants;
using Kvdemy.Core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Core.Dtos
{
    public class CreateTeacherDto
    {
        [Required(ErrorMessage = Message.RequiredField)]
        [Display(Name = Message.FirstName)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = Message.RequiredField)]
        [Display(Name = Message.LastName)]
        public string LastName { get; set; }

        [Required(ErrorMessage = Message.RequiredField)]
        [Display(Name = Message.UserGender)]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = Message.RequiredField)]
        [Display(Name = Message.UserGender)]
        public DateTime DOB { get; set; }

        [Required(ErrorMessage = Message.RequiredField)]
        [Display(Name = Message.UserNationality)]
        public int? NationalityId { get; set; }

        [Required(ErrorMessage = Message.RequiredField)]
        [Display(Name = Message.UserEmail)]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required(ErrorMessage = Message.RequiredField)]
        [Display(Name = Message.UserPassword)]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required(ErrorMessage = Message.RequiredField)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = Message.UserPhoneNumber)]
        public string? PhoneNumber { get; set; }


        [Display(Name = Message.UserLocation)]
        public string? Location { get; set; }

        [Display(Name = Message.UserProfileImage)]
        public IFormFile? ProfileImage { get; set; }

        public string? FCMToken { get; set; }

		public string? NameBase { get; set; }

		public string? Description { get; set; }

		public float? StartingPrice { get; set; }

		public List<ExperienceDto>? Experiences { get; set; }

		public List<EducationDto>? Educations { get; set; }

		public List<CreateAwardDto>? Awards { get; set; }

		public List<DownloadDto>? Downloads { get; set; }

		public int? RegistrationInfoId { get; set; }

		public List<UserSpecialtyDto>? UserSpecialties { get; set; }

		public List<GalleryDto>? Gallery { get; set; }

		public List<VideoDto>? Video { get; set; }

		public List<ContactPhoneNumberDto>? ContactPhoneNumbers { get; set; }

		public string? BookingDetails { get; set; }

		public DateTime CreatedAt { get; set; }

		public UserStatus Status { get; set; }
	}

}

