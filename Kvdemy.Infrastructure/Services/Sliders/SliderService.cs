using AutoMapper;
using Kvdemy.Core.Constants;
using Kvdemy.Core.Dtos;
using Kvdemy.Core.Enums;
using Kvdemy.Core.Exceptions;
using Kvdemy.Core.ViewModels;
using Kvdemy.Data.Models;
using Kvdemy.Web.Data;
using Kvdemy.Web.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Infrastructure.Services.Sliders
{
    public class SliderService : ISliderService
    {

        private readonly KvdemyDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        public SliderService(KvdemyDbContext dbContext, IMapper mapper, IFileService fileService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _fileService = fileService;
        }


        public async Task<List<SliderViewModel>> GetAll()
        {
            IQueryable<Slider> query = _dbContext.Sliders
                         .Where(x => !x.IsDelete)
                         .OrderByDescending(x => x.CreatedAt);

            var modelquery = query.ToList();
            var modelViewModels = _mapper.Map<List<SliderViewModel>>(modelquery);

            //switch (language.ToLower())
            //{
            //    case "en":
            //        foreach (var model in modelmapper)
            //        {
            //            model.Name = model.Name_En;
            //            model.Description = model.Description_En;
            //            if (model.Parent != null)
            //            {
            //                //Parent
            //                model.Parent.Name = model.Parent.Name_En;
            //                model.Parent.Description = model.Parent.Description_En;
            //            }
            //        }
            //        break;
            //    case "ar":
            //        foreach (var model in modelmapper)
            //        {
            //            model.Name = model.Name_Ar;
            //            model.Description = model.Description_Ar;
            //            if (model.Parent != null)
            //            {
            //                //Parent
            //                model.Parent.Name = model.Parent.Name_Ar;
            //                model.Parent.Description = model.Parent.Description_Ar;
            //            }
            //        }
            //        break;
            //    // Add cases for other languages if necessary
            //    default:
            //        foreach (var model in modelmapper)
            //        {
            //            model.Name = model.Name_En;
            //            model.Description = model.Description_En;
            //            if (model.Parent != null)
            //            {
            //                //Parent
            //                model.Parent.Name = model.Parent.Name_En;
            //                model.Parent.Description = model.Parent.Description_En;
            //            }
            //        }
            //        break;
            //}

            return modelViewModels;
        }




        //    public async Task<PagingResultViewModel<SliderViewModel>> GetAll(Core.General.Pagination pagination
        //, QueryDto query)
        //    {
        //        var queryString = _dbContext.Sliders
        //            .Where(x => !x.IsDelete)
        //            .OrderByDescending(x => x.CreatedAt).AsQueryable();

        //        if (!string.IsNullOrWhiteSpace(query.GeneralSearch))
        //        {
        //            queryString = queryString.Where(x => x.Name_En.Contains(query.GeneralSearch)
        //                || x.Name_Ar.Contains(query.GeneralSearch));
        //        }

        //        return await queryString.ToPagedData<SliderViewModel>(pagination, _mapper);
        //    }




        public PagingAPIViewModel GetAll(int page, string language)
        {
            var pageSize = 10;
            var totalmodels = _dbContext.Sliders.Count();
            var totalPages = (int)Math.Ceiling(totalmodels / (double)pageSize);
            if (totalPages != 0)
            {
                // Ensure page number is within valid range
                page = Math.Clamp(page, 1, totalPages);
            }
            var skipCount = (page - 1) * pageSize;

            IQueryable<Slider> query = _dbContext.Sliders
                .Where(x => !x.IsDelete)
                .OrderByDescending(x => x.CreatedAt)
                .Skip(skipCount)
                .Take(pageSize);



            var modelquery = query.ToList();
            var modelViewModels = _mapper.Map<List<SliderViewModel>>(modelquery);
            //switch (language.ToLower())
            //{
            //    case "en":
            //        foreach (var model in modelViewModels)
            //        {
            //            model.Name = model.Name_En;
            //            model.Description = model.Description_En;
            //            if (model.Parent != null)
            //            {
            //                //Parent
            //                model.Parent.Name = model.Parent.Name_En;
            //                model.Parent.Description = model.Parent.Description_En;
            //            }
            //        }
            //        break;
            //    case "ar":
            //        foreach (var model in modelViewModels)
            //        {
            //            model.Name = model.Name_Ar;
            //            model.Description = model.Description_Ar;
            //            if (model.Parent != null)
            //            {
            //                //Parent
            //                model.Parent.Name = model.Parent.Name_Ar;
            //                model.Parent.Description = model.Parent.Description_Ar;
            //            }
            //        }
            //        break;
            //    // Add cases for other languages if necessary
            //    default:
            //        foreach (var model in modelViewModels)
            //        {
            //            model.Name = model.Name_En;
            //            model.Description = model.Description_En;
            //            if (model.Parent != null)
            //            {
            //                //Parent
            //                model.Parent.Name = model.Parent.Name_En;
            //                model.Parent.Description = model.Parent.Description_En;
            //            }
            //        }
            //        break;
            //}
            var pagingResult = new PagingAPIViewModel
            {
                Data = modelViewModels,
                NumberOfPages = totalPages,
                CureentPage = page
            };

            return pagingResult;
        }
        public async Task<int> Create(CreateSliderDto dto)
        {
            if (dto is null)
            {
                throw new InvalidDateException();
            }
            var model = _mapper.Map<Slider>(dto);
            //model.SlugId = Guid.NewGuid().ToString();
            if (dto.Image != null)
            {
                model.Image = await _fileService.SaveFile(dto.Image, FolderNames.ImagesFolder);
            }
            await _dbContext.Sliders.AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return model.Id;
        }
        public async Task<int> Update(UpdateSliderDto dto)
        {
            var model = await _dbContext.Sliders.SingleOrDefaultAsync(x => !x.IsDelete && x.Id == dto.Id);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            if (dto.Title == null)
            {
                dto.Title = model.Title;

            }
            if (dto.Description == null)
            {
                dto.Description = model.Description;

            }
            if (dto.Url == null)
            {
                dto.Url = model.Url;

            }
            if (dto.Type == null)
            {
                dto.Type = model.Type;

            }
            if (dto.Type == SliderType.External)
            {
                dto.InternalId = null;

            }
            if (dto.InternalId == null && dto.Type == SliderType.Internal)
            {
                dto.InternalId = model.InternalId;

            }
            string img = model.Image;

            var updatedmodel = _mapper.Map<UpdateSliderDto, Slider>(dto, model);
            if (dto.Image != null)
            {
                model.Image = await _fileService.SaveFile(dto.Image, FolderNames.ImagesFolder);
            }
            else
            {
                updatedmodel.Image = img;

            }
            _dbContext.Sliders.Update(updatedmodel);
            await _dbContext.SaveChangesAsync();
            return updatedmodel.Id;
        }
        public async Task<int> Delete(int Id)
        {
            var model = await _dbContext.Sliders.SingleOrDefaultAsync(x => x.Id == Id && !x.IsDelete);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            model.IsDelete = true;
            _dbContext.Sliders.Update(model);
            await _dbContext.SaveChangesAsync();
            return model.Id;
        }
        public async Task<SliderViewModel> Get(int Id, string language)
        {
            var model = await _dbContext.Sliders
                .SingleOrDefaultAsync(x => x.Id == Id && !x.IsDelete);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }

            var modelViewModel = _mapper.Map<SliderViewModel>(model);
            //switch (language.ToLower())
            //{
            //    case "en":

            //        modelViewModel.Name = modelViewModel.Name_En;
            //        modelViewModel.Description = modelViewModel.Description_En;
            //        if (modelViewModel.Parent != null)
            //        {
            //            //Parent
            //            modelViewModel.Parent.Name = modelViewModel.Parent.Name_En;
            //            modelViewModel.Parent.Description = modelViewModel.Parent.Description_En;
            //        }


            //        break;
            //    case "ar":

            //        modelViewModel.Name = modelViewModel.Name_Ar;
            //        modelViewModel.Description = modelViewModel.Description_Ar;
            //        if (modelViewModel.Parent != null)
            //        {
            //            //Parent
            //            modelViewModel.Parent.Name = modelViewModel.Parent.Name_Ar;
            //            modelViewModel.Parent.Description = modelViewModel.Parent.Description_Ar;
            //        }
            //        break;
            //    // Add cases for other languages if necessary
            //    default:

            //        modelViewModel.Name = modelViewModel.Name_En;
            //        modelViewModel.Description = modelViewModel.Description_En;
            //        if (modelViewModel.Parent != null)
            //        {
            //            //Parent
            //            modelViewModel.Parent.Name = modelViewModel.Parent.Name_En;
            //            modelViewModel.Parent.Description = modelViewModel.Parent.Description_En;
            //        }
            //        break;
            //}
            return modelViewModel;
        }
        public async Task<UpdateSliderDto> Get(int Id)
        {
            var model = await _dbContext.Sliders
                .SingleOrDefaultAsync(x => x.Id == Id && !x.IsDelete);
            if (model == null)
            {
                throw new EntityNotFoundException();
            }

            var modelViewModel = _mapper.Map<UpdateSliderDto>(model);
            return modelViewModel;
        }
    }
}
