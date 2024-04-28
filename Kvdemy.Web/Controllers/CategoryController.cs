using Kvdemy.Core.Constants;
using Kvdemy.Core.Dtos;
using Kvdemy.Core.Dtos.Helpers;
using Kvdemy.Core.Resourses;
using Kvdemy.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Kvdemy.Web.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly IInterfaceServices _interfaceServices;
        private readonly IStringLocalizer<Messages> _localizedMessages;

        protected string Language;

        public CategoryController(IInterfaceServices interfaceServices, IStringLocalizer<Messages> localizedMessages)
        {
            _interfaceServices = interfaceServices;
            _localizedMessages = localizedMessages;
            Language = Thread.CurrentThread.CurrentUICulture.Name;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SubCategories(int id)
        {
            var result = await _interfaceServices.categoryService.Get(id);

            return View(result);
        }

        public async Task<JsonResult> GetCategoryData(Pagination pagination, Core.Dtos.Helpers.Query query)
        {
            var result = await _interfaceServices.categoryService.GetMainCategories();
            return Json(result);
        }

        public async Task<JsonResult> GetSubCategories(int id)
        {
            TempData["ParentId"] = id;
            var result = await _interfaceServices.categoryService.GetSubCategories(id);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDto dto)
        {
            if (ModelState.IsValid)
            {
                await _interfaceServices.categoryService.Create(dto);
                return Ok(MyResults.AddSuccessResult());
            }
            return View(dto);
        }
        [HttpGet]
        public async Task<IActionResult> CreateSub()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSub(CreateCategoryDto dto)
        {
            if (ModelState.IsValid)
            {
                dto.ParentId = Convert.ToInt32(TempData["ParentId"]);
                await _interfaceServices.categoryService.Create(dto);
                return Ok(MyResults.AddSuccessResult());
            }
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var user = await _interfaceServices.categoryService.Get(id);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateCategoryDto dto)
        {
            if (ModelState.IsValid)
            {
                await _interfaceServices.categoryService.Update(dto);
                return Ok(MyResults.EditSuccessResult());
            }
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _interfaceServices.categoryService.Delete(id);
            return Ok(MyResults.DeleteSuccessResult());
        }
    }
}