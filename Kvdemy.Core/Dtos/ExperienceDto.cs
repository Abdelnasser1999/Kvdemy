using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Core.Dtos
{
	public class ExperienceDto
	{
		public string CompanyName { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string JobTitle { get; set; }
		public string? Description { get; set; }
		public string UserId { get; set; }

	}
}
