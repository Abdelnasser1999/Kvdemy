using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Kvdemy.Web.Controllers
{
	public class HomeController : Controller
	{

		public IActionResult Index()
		{
			return View();
		}

	}
}
