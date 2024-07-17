using Kvdemy.Core.Dtos;
using Kvdemy.Core.ViewModels;
using Kvdemy.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Infrastructure.Services.Teachers
{
    public interface ITeacherService
    {
        Task<dynamic> AddAvailableHoursAsync(string userId, AvailableHoursModel model);
        Task<dynamic> UpdateAvailableHoursAsync(string userId, AvailableHoursModel model);
    }
}
