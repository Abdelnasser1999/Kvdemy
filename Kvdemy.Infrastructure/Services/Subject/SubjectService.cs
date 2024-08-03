using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Kvdemy.Web.Data;
using Kvdemy.Core.ViewModels;
using Kvdemy.Data.Models;
using Kvdemy.Core.Dtos;
using Kvdemy.Core.Exceptions;
using Kvdemy.Infrastructure.Services.Subject;
using Kvdemy.Core.Enums;
using Kvdemy.Core.Constant;
using Kvdemy.Core.Resourses;
using Kvdemy.Web.Services;
using Microsoft.Extensions.Localization;


namespace Krooti.Infrastructure.Services.Subject
{
    public class SubjectService : ISubjectService
    {

        private readonly KvdemyDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IStringLocalizer<Messages> _localizedMessages;

        public SubjectService(KvdemyDbContext dbContext, IMapper mapper
            , IFileService fileService, IStringLocalizer<Messages> localizedMessages)
        {
            _context = dbContext;
            _mapper = mapper;
            _fileService = fileService;
            _localizedMessages = localizedMessages;
        }

        public async Task<dynamic> SearchTeachersAsync(int subcategoryId, string? searchText, string? sortBy)
        {
            var query = _context.Users
                .Include(u => u.UserSpecialties)
                .Where(u => u.UserType == UserType.Teacher && u.UserSpecialties.Any(us => us.SubcategoryId == subcategoryId))
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(u => u.FirstName.Contains(searchText) || u.LastName.Contains(searchText));
            }

            switch (sortBy)
            {
                case "highestRating":
                    query = query.OrderByDescending(u => u.Rating);
                    break;
                case "lowestPrice":
                    query = query.OrderBy(u => u.StartingPrice);
                    break;
                case "highestPrice":
                    query = query.OrderByDescending(u => u.StartingPrice);
                    break;
                default:
                    break;
            }

            var teachers = await query.ToListAsync();

            var result = _mapper.Map<List<TeacherSearchViewModel>>(teachers);

            return new ApiResponseSuccessViewModel(_localizedMessages[MessagesKey.DataSuccess], result);
        }


    }
}
