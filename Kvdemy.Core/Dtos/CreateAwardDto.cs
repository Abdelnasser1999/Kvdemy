using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Core.Dtos
{
	public class CreateAwardDto
	{
		public string Title { get; set; }
		public DateTime Year { get; set; }

		public string UserId { get; set; }
	}
}
