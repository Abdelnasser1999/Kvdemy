using Kvdemy.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Kvdemy.Core.ViewModels
{
    public class SliderViewModel
    {

        public int Id { get; set; }

        [Required]
        public string Image { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Url { get; set; }
        [Required]
        public SliderType Type { get; set; }

        public int? InternalId { get; set; }

    }
}
