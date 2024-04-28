//using Krooti.API.Controllers;
//using Krooti.Core.Constants;
//using Krooti.Infrastructure.Services.Interfaces;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace Krooti.Api.Controllers
//{

//    [ApiController]
//    [AllowAnonymous]
//    public class SettingController : BaseController
//    {
//        private readonly IInterfaceServices _interfaceServices;
//        public SettingController(IInterfaceServices interfaceServices)
//        {
//            _interfaceServices = interfaceServices;
//        }

//        [HttpGet]
//        public async Task<IActionResult> Get()
//        {
//            var data = await _interfaceServices.settingsService.Get();
//            return Ok(GetRespons(data, MessageResults.GetSuccessResult()));
//        }
//    }
//}
