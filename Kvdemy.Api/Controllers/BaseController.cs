using Kvdemy.Infrastructure.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace Kvdemy.API.Controllers
{
    [Route("api/[controller]/[action]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BaseController : Controller
    {
 
         public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

      
        }
        protected APIResponseViewModel GetRespons(object data = null, string message = "Done")
        {
            var result = new APIResponseViewModel();
            result.Status = true;
            result.Message = message;
            result.Data = data;
            return result;
        }
    }
}
