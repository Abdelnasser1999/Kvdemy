using Kvdemy.Core.Dtos;
using Kvdemy.Core.Dtos.Helpers;
using Kvdemy.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Infrastructure.Services.Users
{
    public interface IUserService
    {


        //Task<PagingAPIViewModel> GetAll(string searchKey, int page);
        //Task<PaginationWebViewModel> GetAll(Pagination pagination, Query query);


        Task<dynamic> CreateStudent(CreateStudentDto dto);
        Task<dynamic> CreateTeacher(CreateTeacherDto dto);

        //Task<dynamic> Update(UpdateUserDto dto);
        //Task<dynamic> Update(UpdateUserDto dto, string language);
        //Task<dynamic> Delete(string id);

        //Task<UserViewModel> Get(string Id);
	}
}
