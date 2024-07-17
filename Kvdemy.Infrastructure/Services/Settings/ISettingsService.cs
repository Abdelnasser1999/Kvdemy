using Kvdemy.Core.Dtos;
using Kvdemy.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Infrastructure.Services.Cities
{
    public interface ISettingsService
    {
        Task<SettingsViewModel> Get();
        Task<int> Update(UpdateSettingsDto dto);
    }
}
