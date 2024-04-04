using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Data.Models
{
    public class Gallery
    {
        public int Id { get; set; }

        [Required]
        public string Image { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

    }
}
