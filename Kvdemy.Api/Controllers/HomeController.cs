using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Kvdemy.Api.Controllers
{
	public class HomeController : Controller
	{

		public IActionResult Index()
		{
			return View();
		}

	}
}
