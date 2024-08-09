using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Data.Models
{
    public class Settings : BaseEntity
    {
        public int Id { get; set; }

        public string? About { get; set; }
        public string? Privacy { get; set; }
        public string? Terms { get; set; }
        public double? Dollar { get; set; }
        public double? Euro { get; set; }
        public double? Pound { get; set; }
        public double TaxRate { get; set; }
		public string? FAQ { get; set; }
		public string? Whatsapp { get; set; }


		public Settings()
        {

            TaxRate = 0;
        }   
    }
}
