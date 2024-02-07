using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop_Bear.Models;
using Shop_Bear.Models.EF;
using System.Security.Policy;

namespace Shop_Bear.Controllers
{
	public class ProductsController : Controller
	{
		private readonly ShopBearContext _context;
		public ProductsController(ShopBearContext context)
		{
			_context = context;
		}
		public async Task<IActionResult> Index()
		{
			IEnumerable<Product> items = await _context.Products
					.Include(p => p.ProductImage).Include(p => p.ProductCategory).ToListAsync();

			return View(items);
		}
		public ActionResult Partial_ItemsByCateId()
		{
			var items = _context.Products.Where(x => x.IsHome && x.IsActive).Take(5).ToList();
			return PartialView(items);
		}
		public ActionResult Partial_ProductSales()
		{
			var items = _context.Products.Where(x => x.IsSale && x.IsActive).Take(5).ToList();
			return PartialView(items);
		}
		public async Task<IActionResult> Detail(string alias)
		{
			if(alias == null)
			{
				return RedirectToAction("Index");
			}
			var productByAlias = _context.Products
					.Include(p => p.ProductImage)
					.Include(p => p.ProductCategory)
					.Where(x => x.Alias == alias)
					.FirstOrDefault();
			if(productByAlias != null)
			{
				_context.Products.Attach(productByAlias);
				productByAlias.ViewCount = productByAlias.ViewCount + 1;
				_context.Entry(productByAlias).Property(x => x.ViewCount).IsModified = true;
				_context.SaveChanges();
			}
			return View(productByAlias);
		}
	}
}
