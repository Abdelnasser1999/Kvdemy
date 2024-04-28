using Kvdemy.Core.Constants;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Core.Dtos
{
    public class UpdateCategoryDto
    {

        public int Id { get; set; }
        [Display(Name = Message.CategoryName)]
        public string? Name { get; set; }
        [Display(Name = Message.CategoryImage)]
        public IFormFile? Image { get; set; }
        [Display(Name = Message.CategoryParentId)]
        public int? ParentId { get; set; }
    }
}
