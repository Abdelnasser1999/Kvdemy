using Kvdemy.Core.Constants;
using Kvdemy.Core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Core.ViewModels
{
    public class NotificationViewModel
	{
		public int Id { get; set; }

		public string UserId { get; set; }
		public string Title { get; set; }
		public string Message { get; set; }
		public NotificationType NotificationType { get; set; }
		public bool IsRead { get; set; }
		public bool IsDelete { get; set; }
		public DateTime CreatedAt { get; set; }

	}
}
