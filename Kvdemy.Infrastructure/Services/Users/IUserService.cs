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


		//Task<PagingAPIViewModel> GetStudent(string searchKey, int page);
		Task<dynamic> CreateStudent(CreateStudentDto dto);
		Task<dynamic> CreateTeacher(CreateTeacherDto dto);
		Task<RegisterHelperViewModel> GetRegisterHelper();
		Task<PaginationWebViewModel> GetAllStudents(Pagination pagination, Query query);
		Task<PaginationWebViewModel> GetAllTeachers(Pagination pagination, Query query);
		Task<PaginationWebViewModel> GetNewTeachers(Pagination pagination, Query query);
		Task<dynamic> AcceptTeacher(string id);
		Task<dynamic> RejectTeacher(string id);
        Task<StudentViewModel> GetStudent(string Id);
		Task<TeacherViewModel> GetTeacher(string Id);
		Task<dynamic> GetProfile(string Id);

		//Task<dynamic> Update(UpdateUserDto dto);
		//Task<dynamic> Update(UpdateUserDto dto, string language);
		Task<dynamic> Delete(string id);
		Task<List<NationalityViewModel>> GetNationalities();



    }
}
