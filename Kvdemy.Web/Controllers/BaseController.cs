using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace Kvdemy.Web.Controllers
{
    public class BaseController : Controller
    {
 
         public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

      
        }
    }
}
