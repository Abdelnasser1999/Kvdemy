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
    public class UpdateStudentDto
    {
        [Display(Name = Message.FirstName)]
        public string? FirstName { get; set; }

        [Display(Name = Message.LastName)]
        public string? LastName { get; set; }

        [Display(Name = Message.UserGender)]
        public Gender? Gender { get; set; }

        [Display(Name = Message.UserGender)]
        public DateTime? DOB { get; set; }

        [Display(Name = Message.UserNationality)]
        public int? NationalityId { get; set; }


        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = Message.UserPhoneNumber)]
        public string? PhoneNumber { get; set; }


        [Display(Name = Message.UserLocation)]
        public string? Location { get; set; }


        public string? StudentLanguages { get; set; }


        [Display(Name = Message.UserProfileImage)]
        public IFormFile? ProfileImage { get; set; }

        public string? AdditionalInformation { get; set; }

        public string? FCMToken { get; set; }

    }
}
