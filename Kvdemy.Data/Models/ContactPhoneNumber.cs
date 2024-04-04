using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Data.Models
{
    public class ContactPhoneNumber
    {
        public int Id { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

    }
}
