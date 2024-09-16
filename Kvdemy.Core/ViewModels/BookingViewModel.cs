using Kvdemy.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Core.ViewModels
{
    public class BookingViewModel
    {

		public int Id { get; set; }

		public string StudentId { get; set; }
		public StudentViewModel Student { get; set; }
		public string TeacherId { get; set; }
		public TeacherViewModel Teacher { get; set; }
        public int CategoryId { get; set; }
        public CategoryViewModel Category { get; set; }

        public DateOnly SessionDate { get; set; }
		public string StartTime { get; set; }
		public string EndTime { get; set; }

		public decimal SessionDuration { get; set; }
		public decimal TotalPrice { get; set; }
		public BookingStatus Status { get; set; }
        public string PayPalTransactionId { get; set; }
        public decimal? TotalPriceUSD { get; set; }
        public bool IsRated { get; set; }


    }
}
