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

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public float TotalPrice { get; set; }
        public BookingStatus Status { get; set; }

    }
}
