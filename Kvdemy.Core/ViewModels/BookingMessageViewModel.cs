using Kvdemy.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Core.ViewModels
{
    public class BookingMessageViewModel
    {

        public int Id { get; set; }
        public int BookingId { get; set; }
        public string SenderId { get; set; }
        public SenderViewModel Sender { get; set; }
        public string MessageContent { get; set; }
        public MessageType MessageType { get; set; }
        public string? FileUrl { get; set; }
        public string? FileName { get; set; }
        public string? FileType { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
