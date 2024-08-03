using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Core.Dtos
{
	public class CreateBookingMessageDto
    {
        public int BookingId { get; set; }
        public string SenderId { get; set; }
        public string? MessageContent { get; set; }
        public string MessageType { get; set; }
        public string? FileUrl { get; set; }
    }
}
