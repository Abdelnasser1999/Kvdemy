using Kvdemy.Core.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Data.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string OtpCode { get; set; }


        [Required]
        public Gender Gender { get; set; }

        [Required]
        public DateTime DOB { get; set; }

        public int NationalityId { get; set; }

        public Nationality Nationality { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string? Location { get; set; }

        public string? StudentLanguages { get; set; }

        public string? ProfileImage { get; set; }

        public UserType UserType { get; set; }

        public string? NameBase { get; set; }

        public string? Description { get; set; }

        public float? StartingPrice { get; set; }

        public List<Experience>? Experiences { get; set; }

        public List<Education>? Educations { get; set; }

        public List<Award>? Awards { get; set; }


        public int? RegistrationInfoId { get; set; }
        public RegistrationInfo? RegistrationInfo { get; set; }

        public List<UserSpecialty>? UserSpecialties { get; set; }

        public List<Gallery>? Gallery { get; set; }

        public List<Video>? Video { get; set; }

        public List<ContactPhoneNumber>? ContactPhoneNumbers { get; set; }

        public string? BookingDetails { get; set; }
        public string? AvailableHours { get; set; }
        public string? AdditionalInformation { get; set; }
        public string? FCMToken { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool IsDelete { get; set; }
        public UserStatus Status { get; set; }


        public User()
        {
            CreatedAt = DateTime.Now;
            IsDelete = false;
            Status = UserStatus.inActive;
            Gallery = new List<Gallery>();
            Video = new List<Video>();
            Educations = new List<Education>();

        }

    }
}
