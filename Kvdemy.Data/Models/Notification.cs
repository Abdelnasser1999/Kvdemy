using Kvdemy.Core.Constants;
using Kvdemy.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Data.Models
{
    public class Notification : BaseEntity
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public string Message { get; set; }
        public NotificationType NotificationType { get; set; }
        public bool IsRead { get; set; }

    }
}
