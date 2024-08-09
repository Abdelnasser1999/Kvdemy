using Kvdemy.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Core.ViewModels
{
    public class ReportViewModel
	{

		public int Id { get; set; }
		public string UserId { get; set; }
		public string Message { get; set; }
		public bool IsRead { get; set; }
		public DateTime CreatedAt { get; set; }

	}
}
