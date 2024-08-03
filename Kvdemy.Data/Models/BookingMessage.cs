using Kvdemy.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Data.Models
{
    public class BookingMessage : BaseEntity
    {
        public int Id { get; set; }

        public int BookingId { get; set; }
        public Booking Booking { get; set; }
        public string SenderId { get; set; }
        public User Sender { get; set; }

        public string MessageContent { get; set; }
        public MessageType MessageType { get; set; }
        public string? FileUrl { get; set; }

    }
}
