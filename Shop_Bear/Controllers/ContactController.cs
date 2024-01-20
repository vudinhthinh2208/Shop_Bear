using Microsoft.AspNetCore.Mvc;

namespace Shop_Bear.Controllers
{
	public class ContactController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
