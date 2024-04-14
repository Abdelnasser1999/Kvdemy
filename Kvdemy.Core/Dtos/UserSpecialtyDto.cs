using Kvdemy.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Core.Dtos
{
	public class UserSpecialtyDto
	{
		public string UserId { get; set; }
		public SpecialtyDto Specialty { get; set; }
		public ServiceDto Service { get; set; }
		public float Price { get; set; }
		public string? Description { get; set; }

	}
}
