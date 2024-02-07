using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop_Bear.Models;

namespace Shop_Bear.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize]


	public class ProductImageController : Controller
	{
		private readonly ShopBearContext _context;
		public ProductImageController(ShopBearContext context)
		{
			_context = context;
		}
		public ActionResult Index(int id)
		{
			ViewBag.ProductId = id;
			var items = _context.ProductImages.Where(x => x.ProductId == id).ToList();
			return View(items);
		}
        [HttpPost]
        public ActionResult AddImage(int productId, string url)
		{
			_context.ProductImages.Add(new Models.EF.ProductImage
			{
				ProductId = productId,
				Image = url,
				IsDefault = false
			});
			_context.SaveChanges();
			return Json(new { Success = true });
		}
		
		public ActionResult Delete(int id)
		{
			var item = _context.ProductImages.Find(id);
			_context.ProductImages.Remove(item);
			_context.SaveChanges();
			return Json(new {success =true });
		}
	}
}
