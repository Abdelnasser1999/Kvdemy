using Kvdemy.API.Controllers;
using Kvdemy.Core.Constants;
using Kvdemy.Core.Dtos;
using Kvdemy.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Kvdemy.Api.Controllers
{
    [ApiController]

    public class CategoryController : BaseController
    {

        private readonly IInterfaceServices _interfaceServices;
        public CategoryController(IInterfaceServices interfaceServices)
        {
            _interfaceServices = interfaceServices;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAll(string lang = Constant.English, int page = Constant.NumberOne)
        {
            var data = _interfaceServices.categoryService.GetAll(page, lang);
            return Ok(GetRespons(data, MessageResults.GetSuccessResult()));
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> SubCategories(int ParentId)
        {
            var result = await _interfaceServices.categoryService.GetSubCategories(ParentId);
            return Ok(GetRespons(result, MessageResults.GetSuccessResult()));
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCategoryDto dto)
        {
            if (ModelState.IsValid)
            {
                var data  =  await _interfaceServices.categoryService.Create(dto);
                return Ok(GetRespons(data, MessageResults.AddSuccessResultString()));
            }
            return Ok(dto);
        }


        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateCategoryDto dto)
        {
            if (ModelState.IsValid)
            {
               var data = await _interfaceServices.categoryService.Update(dto);
                return Ok(GetRespons(data,MessageResults.EditSuccessResultString()));
            }
            return Ok(dto);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _interfaceServices.categoryService.Delete(id);
            return Ok(GetRespons(MessageResults.DeleteSuccessResult()));
        }


        [HttpGet]
        public async Task<IActionResult> Get(int id, string lang = Constant.English)
        {
            var data = await _interfaceServices.categoryService.Get(id, lang);


            return Ok(GetRespons(data, MessageResults.GetSuccessResult()));
        }
    }
}
