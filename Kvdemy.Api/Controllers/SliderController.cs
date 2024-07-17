using Kvdemy.API.Controllers;
using Kvdemy.Core.Constants;
using Kvdemy.Core.Dtos;
using Kvdemy.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kvdemy.Api.Controllers
{
    public class SliderController : BaseController
    {

        private readonly IInterfaceServices _interfaceServices;
        public SliderController(IInterfaceServices interfaceServices)
        {
            _interfaceServices = interfaceServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _interfaceServices.sliderService.GetAll();
            return Ok(GetRespons(data, MessageResults.GetSuccessResult()));
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateSliderDto dto)
        {
            if (ModelState.IsValid)
            {
                var data = await _interfaceServices.sliderService.Create(dto);
                return Ok(GetRespons(data, MessageResults.AddSuccessResultString()));
            }
            return Ok(dto);
        }


    }
}