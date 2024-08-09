using AutoMapper;
using Kvdemy.Core.Constant;
using Kvdemy.Core.Constants;
using Kvdemy.Core.Dtos;
using Kvdemy.Core.Dtos.Helpers;
using Kvdemy.Core.Exceptions;
using Kvdemy.Core.Resourses;
using Kvdemy.Core.ViewModels;
using Kvdemy.Data.Models;
using Kvdemy.Web.Data;
using Kvdemy.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Infrastructure.Services.Reports
{
    public class ReportService : IReportService
	{

        private readonly KvdemyDbContext _dbContext;
        private readonly IMapper _mapper;
		private readonly IStringLocalizer<Messages> _localizedMessages;

		public ReportService(KvdemyDbContext dbContext, IMapper mapper, IStringLocalizer<Messages> localizedMessages)
        {
            _dbContext = dbContext;
            _mapper = mapper;
			_localizedMessages = localizedMessages;

		}

		public async Task<dynamic> AddReportAsync(CreateReportDto dto)
		{
			var report = _mapper.Map<Report>(dto);

			report.CreatedAt = DateTime.UtcNow;
			report.IsRead = false;

			_dbContext.Reports.Add(report);
			await _dbContext.SaveChangesAsync();
			var result = _mapper.Map<ReportViewModel>(report);
			return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.ItemCreatedSuccss],result);

		}
		public async Task<dynamic> MarkAsReadAsync(int reportId)
		{
			var report = await _dbContext.Reports.FindAsync(reportId);
			if (report == null)
			{
				return false;
			}

			report.IsRead = true;
			_dbContext.Reports.Update(report);
			await _dbContext.SaveChangesAsync();

			return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.ItemUpdatedSuccss]);
		}

	}
}
