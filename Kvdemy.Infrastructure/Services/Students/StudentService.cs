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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Infrastructure.Services.Students
{
    public class StudentService : IStudentService
    {
        private readonly KvdemyDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IStringLocalizer<Messages> _localizedMessages;

        public StudentService(KvdemyDbContext dbContext, IMapper mapper
            , IFileService fileService, IStringLocalizer<Messages> localizedMessages)
        {
            _context = dbContext;
            _mapper = mapper;
            _fileService = fileService;
            _localizedMessages = localizedMessages;
        }

        public async Task<dynamic> UpdateStudentAsync(string id , UpdateStudentGeneralInfoDto dto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null || user.UserType != UserType.Student)
                return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.ItemNotFound]);

            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.Email = dto.Email;
            user.PhoneNumber = dto.PhoneNumber;
            user.Location = dto.Location;
            user.DOB = dto.DOB;
            user.NationalityId = dto.NationalityId;
            user.Gender = dto.Gender;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.ItemUpdatedSuccss]);
        }

        public async Task<dynamic> GetProfileAsync(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null || user.UserType != UserType.Student)
                return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.ItemNotFound]);

            var profile = new UpdateStudentGeneralInfoDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Location = user.Location,
                DOB = user.DOB,
                NationalityId = user.NationalityId,
                Gender = user.Gender
            };

            return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.DataSuccess], profile);
        }

        public async Task<dynamic> UpdateProfileImageAsync(string userId, ProfileImageDto imageDto)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null || user.UserType != UserType.Student)
                return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.ItemNotFound]);

            var imageUrl = await _fileService.SaveFile(imageDto.ProfileImage, FolderNames.ImagesFolder);
            user.ProfileImage = imageUrl;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.ItemUpdatedSuccss]);
        }
        public async Task<dynamic> GetProfileImageAsync(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null || user.UserType != UserType.Student)
                return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.ItemNotFound]);

            return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.DataSuccess], user.ProfileImage);
        }
        public async Task<dynamic> UpdateAdditionalInformationAsync(string userId, string AdditionalInformation)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null || user.UserType != UserType.Student)
                return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.ItemNotFound]);

            user.AdditionalInformation = AdditionalInformation;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.ItemUpdatedSuccss]);
        }

        public async Task<dynamic> GetAdditionalInformationAsync(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null || user.UserType != UserType.Student)
                return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.ItemNotFound]);
            var model = _mapper.Map<UserViewModel>(user);

            // Attempt to deserialize the AdditionalInformation string to a dynamic object
            if (!string.IsNullOrEmpty(model.AdditionalInformation))
            {
                try
                {
                    model.AdditionalInformation = JsonConvert.DeserializeObject<dynamic>(model.AdditionalInformation);
                }
                catch (JsonReaderException ex)
                {
                    // Log the exception and handle the invalid JSON case
                    // For example, you can log the error and set additionalInfo to null
                    Console.WriteLine($"Error deserializing AdditionalInformation: {ex.Message}");
                }
            }

            return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.DataSuccess], model.AdditionalInformation);

        }
        public async Task<dynamic> UpdateStudentLanguagesAsync(string userId, string StudentLanguages)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null || user.UserType != UserType.Student)
                return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.ItemNotFound]);

            user.StudentLanguages = StudentLanguages;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.ItemUpdatedSuccss]);
        }

        public async Task<dynamic> GetStudentLanguagesAsync(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null || user.UserType != UserType.Student)
                return new ApiResponseFailedViewModel(_localizedMessages[MessagesKey.ItemNotFound]);
            var model = _mapper.Map<UserViewModel>(user);

            // Attempt to deserialize the AdditionalInformation string to a dynamic object
            if (!string.IsNullOrEmpty(model.StudentLanguages))
            {
                try
                {
                    model.AdditionalInformation = JsonConvert.DeserializeObject<dynamic>(model.StudentLanguages);
                }
                catch (JsonReaderException ex)
                {
                    // Log the exception and handle the invalid JSON case
                    // For example, you can log the error and set additionalInfo to null
                    Console.WriteLine($"Error deserializing AdditionalInformation: {ex.Message}");
                }
            }

            return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.DataSuccess], model.StudentLanguages);

        }
    }
}
