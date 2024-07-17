using Kvdemy.Core.Constants;
using Kvdemy.Core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Core.Dtos
{
    public class UpdateSliderDto
    {

        public int Id { get; set; }
        [Display(Name = Message.SliderTitle)]
        public string? Title { get; set; }
        [Display(Name = Message.SliderImage)]
        public IFormFile? Image { get; set; }
        [Display(Name = Message.SliderDescription)]
        public string? Description { get; set; }
        [Display(Name = Message.SliderUrl)]
        public string? Url { get; set; }
        [Display(Name = Message.SliderType)]
        public SliderType? Type { get; set; }
        [Display(Name = Message.SliderInternalId)]
        public int? InternalId { get; set; }

    }
}
