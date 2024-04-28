using Kvdemy.Core.Dtos;
using Kvdemy.Core.Dtos.Helpers;
using Kvdemy.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Infrastructure.Services.Categories
{
    public interface ICategoryService
    {
        Task<List<CategoryViewModel>> GetAll(string? GeneralSearch, string language);
        Task<PaginationWebViewModel> GetAll(Pagination pagination, Query query);
        PagingAPIViewModel GetAll(int page, string language);
        Task<List<CategoryViewModel>> GetMainCategories();
        Task<List<CategoryViewModel>> GetSubCategories(int parentId);

		Task<CategoryViewModel> Create(CreateCategoryDto dto);
        Task<CategoryViewModel> Update(UpdateCategoryDto dto);
        Task<int> Delete(int Id);
        Task<CategoryViewModel> Get(int Id, string language);
        Task<UpdateCategoryDto> Get(int Id);
    }
}
