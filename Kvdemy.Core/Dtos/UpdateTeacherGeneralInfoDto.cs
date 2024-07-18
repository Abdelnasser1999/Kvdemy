using Kvdemy.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Core.Dtos
{
	public class UpdateTeacherGeneralInfoDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? NameBase { get; set; }
        public string? Location { get; set; }
        public DateTime DOB { get; set; }
        public int NationalityId { get; set; }
        public Gender Gender { get; set; }

    }
}
