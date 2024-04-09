using Kvdemy.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Core.ViewModels
{
    public class AdminViewModel
    {

        public string Id { get; set; }
        public string FullName_en { get; set; }

        public UserType UserType { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string? ProfileImage { get; set; }
        public string? FCMToken { get; set; }

    }
}
