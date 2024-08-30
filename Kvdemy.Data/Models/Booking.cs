using Kvdemy.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Data.Models
{
    public class Booking : BaseEntity
    {
        public int Id { get; set; }

        public string StudentId { get; set; }
        public User Student { get; set; }
        public string TeacherId { get; set; }
        public User Teacher { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public DateOnly SessionDate { get; set; }
		public string StartTime { get; set; }
		public string EndTime { get; set; }

		public decimal SessionDuration { get; set; }
        public decimal TotalPrice { get; set; }
        public BookingStatus Status { get; set; }
        public string PayPalTransactionId { get; set; }
        public string PayPalPayerID { get; set; }

    }
}
