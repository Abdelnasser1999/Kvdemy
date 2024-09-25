using Kvdemy.Core.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Kvdemy.Core.ViewModels
{
    public class SettingsViewModel
    {

        public int Id { get; set; }
        public string? About { get; set; }
        public string? Privacy { get; set; }
        public string? Terms { get; set; }
		public string? FAQ { get; set; }
		public string? Whatsapp { get; set; }

        public int? MinimumWithdrawal { get; set; }
        public double? Commission { get; set; }
        public decimal? USD { get; set; }


    }
}
