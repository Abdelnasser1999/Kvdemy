﻿using Kvdemy.Core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Core.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OtpCode { get; set; }
        public Gender Gender { get; set; }
        public DateTime DOB { get; set; }
        public int NationalityId { get; set; }
        public NationalityViewModel Nationality { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? Location { get; set; }
        public string? StudentLanguages { get; set; }
        public string? ProfileImage { get; set; }
        public UserType UserType { get; set; }
        public string? NameBase { get; set; }
        public string? Description { get; set; }
        public float? StartingPrice { get; set; }
        public List<ExperienceViewModel>? Experiences { get; set; }

        public List<EducationViewModel>? Educations { get; set; }

        public List<AwardViewModel>? Awards { get; set; }

        public List<DownloadViewModel>? Downloads { get; set; }

        public int? RegistrationInfoId { get; set; }
        public List<UserSpecialtyViewModel>? UserSpecialties { get; set; }

        public List<GalleryViewModel>? Gallery { get; set; }

        public List<VideoViewModel>? Video { get; set; }

        public List<ContactPhoneNumberViewModel>? ContactPhoneNumbers { get; set; }
        public string? BookingDetails { get; set; }
        public dynamic? AdditionalInformation { get; set; }
        public string? FCMToken { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool IsDelete { get; set; }
        public UserStatus Status { get; set; }
        public FinanceAccountViewModel FinanceAccount { get; set; }
    }
}
