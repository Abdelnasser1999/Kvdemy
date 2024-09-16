using Kvdemy.API.Controllers;
using Kvdemy.Core.Constants;
using Kvdemy.Core.Dtos;
using Kvdemy.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kvdemy.Api.Controllers
{

    [ApiController]
    public class FinanceAccountController : BaseController
    {
        private readonly IInterfaceServices _interfaceServices;
        public FinanceAccountController(IInterfaceServices interfaceServices)
        {
            _interfaceServices = interfaceServices;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetWalletTransaction()
        {
            var data = await _interfaceServices.financeAccountService.GetWalletTransaction();
            return Ok(GetRespons(data, MessageResults.GetSuccessResult()));
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddWalletRequest(double amount)
        {
            var data = await _interfaceServices.financeAccountService.AddWalletRequest(amount);
            return Ok(data);
        }

    }
}
