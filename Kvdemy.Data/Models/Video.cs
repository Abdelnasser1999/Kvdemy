using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Data.Models
{
    public class Video
    {
        public int Id { get; set; }

        [Required]
        public string Url { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

    }
}
