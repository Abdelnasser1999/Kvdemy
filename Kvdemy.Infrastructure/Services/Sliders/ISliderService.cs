using Kvdemy.Core.Dtos;
using Kvdemy.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kvdemy.Infrastructure.Services.Sliders
{
    public interface ISliderService
    {
        Task<List<SliderViewModel>> GetAll();
        PagingAPIViewModel GetAll(int page, string language);
        Task<int> Create(CreateSliderDto dto);
        Task<int> Update(UpdateSliderDto dto);
        Task<int> Delete(int Id);
        Task<SliderViewModel> Get(int Id, string language);
        Task<UpdateSliderDto> Get(int Id);
    }
}
