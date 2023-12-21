using Microsoft.AspNetCore.Mvc;
using Shop_Bear.Models;

namespace Shop_Bear.Areas.Admin.Controllers
{
	[Area("Admin")]

	public class ProductImageController : Controller
	{
		private readonly ShopBearContext _context;
		public ProductImageController(ShopBearContext context)
		{
			_context = context;
		}
		public ActionResult Index(int id)
		{
			var items = _context.ProductImages.Where(x => x.ProductId == id).ToList();
			return View();
		}
	}
}
