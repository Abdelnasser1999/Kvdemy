using System;
using System.Collections.Generic;
using System.Text;

namespace Kvdemy.Infrastructure.Helpers
{
    public class AvailableHoursModel
    {
		public Dictionary<string, List<TimeRange>> AvailableHours { get; set; }
	}

	public class TimeRange
    {
        public string From { get; set; }
        public string To { get; set; }
    }
}
