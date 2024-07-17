using AutoMapper;
using Kvdemy.Core.Constant;
using Kvdemy.Core.Constants;
using Kvdemy.Core.Dtos;
using Kvdemy.Core.Enums;
using Kvdemy.Core.Exceptions;
using Kvdemy.Core.Resourses;
using Kvdemy.Core.ViewModels;
using Kvdemy.Data.Models;
using Kvdemy.Infrastructure.Helpers;
using Kvdemy.Web.Data;
using Kvdemy.Web.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Infrastructure.Services.Teachers
{
    public class TeacherService : ITeacherService
    {
        private readonly KvdemyDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IStringLocalizer<Messages> _localizedMessages;

        public TeacherService(KvdemyDbContext dbContext, IMapper mapper
            , IFileService fileService, IStringLocalizer<Messages> localizedMessages)
        {
            _context = dbContext;
            _mapper = mapper;
            _fileService = fileService;
            _localizedMessages = localizedMessages;
        }
        public async Task<dynamic> AddAvailableHoursAsync(string userId, AvailableHoursModel model)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null || user.UserType != UserType.Teacher)
            {
                return new ApiResponseFailedViewModel("user not found");
            }

            user.AvailableHours = JsonConvert.SerializeObject(model.AvailableHours);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.ItemCreatedSuccss]);

        }

        public async Task<dynamic> UpdateAvailableHoursAsync(string userId, AvailableHoursModel model)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null || user.UserType != UserType.Teacher)
            {
                return new ApiResponseFailedViewModel("user not found");
            }

            user.AvailableHours = JsonConvert.SerializeObject(model.AvailableHours);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.ItemCreatedSuccss]);
        }
    }
}
