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
        public string UserId { get; set; }
        public int CategoryId { get; set; }
        public int SubcategoryId { get; set; }

        public User User { get; set; }
        public Category Category { get; set; }
        public Category Subcategory { get; set; }
    }
}
