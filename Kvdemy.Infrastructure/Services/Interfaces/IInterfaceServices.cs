using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kvdemy.Web.Services;
using Kvdemy.Infrastructure.Services.Users;
using Kvdemy.Infrastructure.Services.Auth;
using Kvdemy.Infrastructure.Services.Categories;
using Kvdemy.Infrastructure.Services.Cities;
using Kvdemy.Infrastructure.Services.Sliders;
using Kvdemy.Infrastructure.Services.Teachers;
using Kvdemy.Infrastructure.Services.Students;


namespace Kvdemy.Infrastructure.Services.Interfaces
{
    public interface IInterfaceServices
    {

        IFileService fileService { get; }
        IUserService userService { get; }

        IAuthService authService { get; }
		ICategoryService categoryService { get; }
		ISettingsService settingsService { get; }
		ISliderService sliderService { get; }
		ITeacherService teacherService { get; }
		IStudentService studentService { get; }

	}
}
