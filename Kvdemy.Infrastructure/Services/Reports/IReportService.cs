using Kvdemy.Core.Dtos;
using Kvdemy.Core.Dtos.Helpers;
using Kvdemy.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Infrastructure.Services.Reports
{
    public interface IReportService
	{
		Task<dynamic> AddReportAsync(CreateReportDto dto);
		Task<dynamic> MarkAsReadAsync(int reportId);

	}
}
