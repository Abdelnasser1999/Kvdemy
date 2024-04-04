using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Data.Models
{
    public class RegistrationInfo
    {
        public int Id { get; set; }

        [Required]
        public string RegistrationNumber { get; set; }

        [Required]
        public string Document { get; set; }

        [Required]
        public bool isVerified { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

    }
}
