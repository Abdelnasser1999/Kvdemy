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
    public class UpdateSettingsDto
    {


        public string? About { get; set; }
        public string? Privacy { get; set; }
        public string? Terms { get; set; }
		public string? FAQ { get; set; }
		public string? Whatsapp { get; set; }

		[Display(Name = Message.SettingsDollar)]
        public double? Dollar { get; set; }
        [Display(Name = Message.SettingsEuro)]
        public double? Euro { get; set; }
        [Display(Name = Message.SettingsPound)]
        public double? Pound { get; set; }


    }
}
