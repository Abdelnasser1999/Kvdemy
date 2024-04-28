using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Kvdemy.Core.ViewModels
{
    public class CategoryViewModel
    {

        public int Id { get; set; }

        public string Name { get; set; }
        public string Image { get; set; }
        public int ParentId { get; set; }
        public CategoryViewModel Parent { get; set; }
      
    }
}
