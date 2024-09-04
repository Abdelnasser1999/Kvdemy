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
        [Required]
        public string StudentId { get; set; }
        public User Student { get; set; }
        [Required]
        public string TeacherId { get; set; }
        public User Teacher { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [Required]
        public DateOnly SessionDate { get; set; }
        [Required]
        public string StartTime { get; set; }
        [Required]
        public string EndTime { get; set; }
        [Required]
        public decimal SessionDuration { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }
        [Required]
        public BookingStatus Status { get; set; }
        public string? PayPalTransactionId { get; set; }
        public string? PayPalPayerID { get; set; }

    }
}
