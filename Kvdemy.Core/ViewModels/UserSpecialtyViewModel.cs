using Kvdemy.Core.Constants;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Core.ViewModels
{
    public class UserSpecialtyViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int CategoryId { get; set; }
        public int SubcategoryId { get; set; }

        public UserViewModel User { get; set; }
        public CategoryViewModel Category { get; set; }
        public CategoryViewModel Subcategory { get; set; }
    }
}
