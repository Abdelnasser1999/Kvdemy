using Krooti.Core.Enums;
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

        public List<StudentLanguage> StudentLanguages { get; set; }

        public string? ProfileImage { get; set; }

        public UserType UserType { get; set; }

        public string? NameBase { get; set; }

        public string? Description { get; set; }

        public float? StartingPrice { get; set; }

        public List<Experience>? Experiences { get; set; }

        public List<Education>? Educations { get; set; }

        public List<Award>? Awards { get; set; }

        public List<Download>? Downloads { get; set; }

        public int? RegistrationInfoId { get; set; }
        public RegistrationInfo? RegistrationInfo { get; set; }

        public List<UserSpecialty>? UserSpecialties { get; set; }

        public List<Gallery>? Gallery { get; set; }

        public List<Video>? Video { get; set; }

        public List<ContactPhoneNumber>? ContactPhoneNumbers { get; set; }

        public string? BookingDetails { get; set; }
        public string? AdditionalInformation { get; set; }
        public string? FCMToken { get; set; }

    }
}
