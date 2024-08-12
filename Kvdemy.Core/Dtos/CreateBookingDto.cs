using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Core.Dtos
{
	public class CreateBookingDto
    {
        public string StudentId { get; set; }
        public string TeacherId { get; set; }
		public DateOnly SessionDate { get; set; }
		public string StartTime { get; set; }
		public string EndTime { get; set; }
    }
}
