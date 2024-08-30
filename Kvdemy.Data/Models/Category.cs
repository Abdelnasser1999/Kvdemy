using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Data.Models
{
    public class Category : BaseEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Image { get; set; }

        public int? ParentId { get; set; }

        public Category Parent{ get; set; }
        public List<Category> Subcategories { get; set; }
        public List<Booking> Bookings { get; set; }

    }
}
