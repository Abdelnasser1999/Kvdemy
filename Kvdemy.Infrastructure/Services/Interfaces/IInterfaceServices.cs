using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Krooti.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kvdemy.Web.Services;
using Kvdemy.Infrastructure.Services.Users;
using Kvdemy.Infrastructure.Services.Auth;


namespace Krooti.Infrastructure.Services.Interfaces
{
    public interface IInterfaceServices
    {

        IFileService fileService { get; }
        IUserService userService { get; }

        IAuthService authService { get; }

    }
}
