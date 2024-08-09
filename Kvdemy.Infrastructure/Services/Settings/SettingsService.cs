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
using Kvdemy.Infrastructure.Services.Cities;


namespace Krooti.Infrastructure.Services.Cities
{
    public class SettingsService : ISettingsService
    {

        private readonly KvdemyDbContext _dbContext;
        private readonly IMapper _mapper;
      
        public SettingsService(KvdemyDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
      
        }


        public async Task<SettingsViewModel> Get()
        {
            IQueryable<Settings> query = _dbContext.Settings;

            var model = query.FirstOrDefault();
            var modelViewModels = _mapper.Map<SettingsViewModel>(model);


            return modelViewModels;
        }
        public async Task<int> Update(UpdateSettingsDto dto)
        {
            var model = await _dbContext.Settings.FirstOrDefaultAsync();
            if (model == null)
            {
                throw new EntityNotFoundException();
            }
            if (dto.Privacy == null)
            {
                dto.Privacy = model.Privacy;

            }
            if (dto.Terms == null)
            {
                dto.Terms = model.Terms;

            }
            if (dto.About == null)
            {
                dto.About = model.About;

            }
            if (dto.FAQ == null)
            {
                dto.FAQ = model.FAQ;

            }
            if (dto.Whatsapp == null)
            {
                dto.Whatsapp = model.Whatsapp;

            }

            var updatedmodel = _mapper.Map<UpdateSettingsDto, Settings>(dto, model);

            _dbContext.Settings.Update(updatedmodel);
            await _dbContext.SaveChangesAsync();
            return updatedmodel.Id;
        }

    }
}
