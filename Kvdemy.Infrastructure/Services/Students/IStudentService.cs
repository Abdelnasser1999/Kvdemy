using Kvdemy.Core.Dtos;
using Kvdemy.Core.ViewModels;
using Kvdemy.Infrastructure.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Infrastructure.Services.Students
{
    public interface IStudentService
    {
        Task<dynamic> UpdateStudentAsync(string id, UpdateStudentGeneralInfoDto dto);
        Task<dynamic> GetProfileAsync(string userId);
        Task<dynamic> UpdateProfileImageAsync(string userId, ProfileImageDto imageDto);
        Task<dynamic> GetProfileImageAsync(string userId);
        Task<dynamic> UpdateAdditionalInformationAsync(string userId, string AdditionalInformation);
        Task<dynamic> GetAdditionalInformationAsync(string userId);
        Task<dynamic> UpdateStudentLanguagesAsync(string userId, string AdditionalInformation);
        Task<dynamic> GetStudentLanguagesAsync(string userId);
    }
}
