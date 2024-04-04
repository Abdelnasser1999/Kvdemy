using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Data.Models
{
    public class Education
    {
        public int Id { get; set; }

        [Required]
        public string InstituteName { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string DegreeTitle { get; set; }

        public string? Description { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

    }
}
