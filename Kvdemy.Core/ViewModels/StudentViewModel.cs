using Kvdemy.Core.Constants;
using Kvdemy.Core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Core.ViewModels
{
    public class StudentViewModel
    {

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime DOB { get; set; }
        public int NationalityId { get; set; }
        public NationalityViewModel Nationality { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? Location { get; set; }
        public string? StudentLanguages { get; set; }
        public string? ProfileImage { get; set; }
        public string? AdditionalInformation { get; set; }
        public string? FCMToken { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserStatus Status { get; set; }


    }
}
