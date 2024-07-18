using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Core.Dtos
{
	public class EducationDto
	{
		public string InstituteName { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string DegreeTitle { get; set; }
		public string? Description { get; set; }

    }
}
