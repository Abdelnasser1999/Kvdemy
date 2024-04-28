using AutoMapper;
using Kvdemy.Core.Constants;
using Kvdemy.Core.Dtos;
using Kvdemy.Core.Dtos.Helpers;
using Kvdemy.Core.Exceptions;
using Kvdemy.Core.ViewModels;
using Kvdemy.Data.Models;
using Kvdemy.Web.Data;
using Kvdemy.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Infrastructure.Services.Categories
{
    public class CategroyService : ICategoryService
    {

        private readonly KvdemyDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        public CategroyService(KvdemyDbContext dbContext, IMapper mapper, IFileService fileService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _fileService = fileService;
        }


        public async Task<List<CategoryViewModel>> GetAll(string? GeneralSearch, string language)
        {
            IQueryable<Category> query = _dbContext.Categories
                .Include(x => x.Parent)
                .Where(x => !x.IsDelete
                && (string.IsNullOrWhiteSpace(GeneralSearch)
                || x.Name.Contains(GeneralSearch)))
                .OrderByDescending(x => x.CreatedAt);

            var modelquery =  query.ToList();
            var modelmapper = _mapper.Map<List<CategoryViewModel>>(modelquery);

            return modelmapper;
        }

        public async Task<List<CategoryViewModel>> GetMainCategories()
        {
            IQueryable<Category> query = _dbContext.Categories
                .Where(x => !x.IsDelete
                && x.ParentId == null)
                .OrderByDescending(x => x.CreatedAt);

            var modelquery =  query.ToList();
            var modelmapper = _mapper.Map<List<CategoryViewModel>>(modelquery);

            return modelmapper;
        }
        public async Task<List<CategoryViewModel>> GetSubCategories(int parentId)
        {
            IQueryable<Category> query = _dbContext.Categories
                .Where(x => !x.IsDelete
                && x.ParentId == parentId)
                .OrderByDescending(x => x.CreatedAt);

            var modelquery =  query.ToList();
            var modelmapper = _mapper.Map<List<CategoryViewModel>>(modelquery);

            return modelmapper;
        }


        public async Task<PaginationWebViewModel> GetAll(Pagination pagination, Query query)
        {
            var queryString = _dbContext.Categories.Where(x => !x.IsDelete && (x.Name.Contains(query.GeneralSearch) || string.IsNullOrWhiteSpace(query.GeneralSearch))).AsQueryable();

            var dataCount = queryString.Count();
            var skipValue = pagination.GetSkipValue();
            var dataList = await queryString.Skip(skipValue).Take(pagination.PerPage).ToListAsync();
            var categories = _mapper.Map<List<CategoryViewModel>>(dataList);
            var pages = pagination.GetPages(dataCount);
            var result = new PaginationWebViewModel
            {
                data = categories,
                meta = new Meta
                {
                    page = pagination.Page,
                    perpage = pagination.PerPage,
                    pages = pages,
                    total = dataCount,
                }
            };
            return result;
        }

        public PagingAPIViewModel GetAll(int page, string language)
        {
            var pageSize = 10;
            var totalmodels = _dbContext.Categories.Count();
            var totalPages = (int)Math.Ceiling(totalmodels / (double)pageSize);
            if (totalPages != 0)
            {
                // Ensure page number is within valid range
                page = Math.Clamp(page, 1, totalPages);
            }
            var skipCount = (page - 1) * pageSize;

            IQueryable<Category> query = _dbContext.Categories
                .Include(x => x.Subcategories)
                .Where(x => !x.IsDelete)
                .OrderByDescending(x => x.CreatedAt)
                .Skip(skipCount)
                .Take(pageSize);



            var modelquery = query.ToList();
            var modelViewModels = _mapper.Map<List<CategoryViewModel>>(modelquery);
            var pagingResult = new PagingAPIViewModel
            {
                Data = modelViewModels,
                NumberOfPages = totalPages,
                CureentPage = page
            };

            return pagingResult;
        }
        public async Task<CategoryViewModel> Create(CreateCategoryDto dto)
        {
            if (dto is null)
            {
                throw new InvalidDateException();
            }
            if (dto.ParentId == 0)
            {
                dto.ParentId = null;

            }
            var model = _mapper.Map<Category>(dto);
            //model.SlugId = Guid.NewGuid().ToString();
            if (dto.Image != null)
            {
                model.Image = await _fileService.SaveFile(dto.Image, FolderNames.ImagesFolder);
            }
            await _dbContext.Categories.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            var modelvm = _mapper.Map<CategoryViewModel>(model);

            return modelvm;
        }
        public async Task<CategoryViewModel> Update(UpdateCategoryDto dto)
        {
            var model = await _dbContext.Categories.SingleOrDefaultAsync(x => !x.IsDelete && x.Id == dto.Id);


            if (model == null)
            {

                throw new EntityNotFoundException();
            }

            string img = model.Image;

            if (dto.Name == null)
            {
                dto.Name = model.Name;

            }
            if (dto.ParentId == 0 || dto.ParentId == null)
            {
                dto.ParentId = model.ParentId;

            }
            var updatedmodel = _mapper.Map<UpdateCategoryDto, Category>(dto, model);
            if (dto.Image != null)
            {
                model.Image = await _fileService.SaveFile(dto.Image, FolderNames.ImagesFolder);
            }else
            {
                updatedmodel.Image = img;

            }
            _dbContext.Categories.Update(updatedmodel);
            await _dbContext.SaveChangesAsync();

          var modelvm =  _mapper.Map<CategoryViewModel>(updatedmodel);

            return modelvm;
        }
        public async Task<int> Delete(int Id)
        {
            var model = await _dbContext.Categories.SingleOrDefaultAsync(x => x.Id == Id && !x.IsDelete);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            model.IsDelete = true;
            _dbContext.Categories.Update(model);
            await _dbContext.SaveChangesAsync();
            return model.Id;
        }
        public async Task<CategoryViewModel> Get(int Id, string language)
        {
            var model = await _dbContext.Categories
                .Include(x => x.Parent)
                .SingleOrDefaultAsync(x => x.Id == Id && !x.IsDelete);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }

            var modelViewModel = _mapper.Map<CategoryViewModel>(model);
            return modelViewModel;
        }
        public async Task<UpdateCategoryDto> Get(int Id)
        {
            var model = await _dbContext.Categories
                .Include(x => x.Parent)
                .SingleOrDefaultAsync(x => x.Id == Id && !x.IsDelete);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }

            var modelViewModel = _mapper.Map<UpdateCategoryDto>(model);
            return modelViewModel;
        }
    }
}
