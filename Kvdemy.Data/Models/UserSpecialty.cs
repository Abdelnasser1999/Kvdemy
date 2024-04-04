using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Data.Models
{
    public class UserSpecialty
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int SpecialtyId { get; set; }
        public Specialty Specialty { get; set; }

        public int ServiceId { get; set; }
        public Service Service { get; set; }

        [Required]
        public float Price { get; set; }
        public string? Description { get; set; }
    }
}
