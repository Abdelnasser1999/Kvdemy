using Kvdemy.Core.Dtos;
using Kvdemy.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Infrastructure.Services.Subject
{
    public interface ISubjectService
    {
        Task<dynamic> SearchTeachersAsync(int subcategoryId, string? searchText, string? sortBy);
    }
}
