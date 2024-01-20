using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop_Bear.Models;
using Shop_Bear.Models.EF;

namespace Shop_Bear.Repository.Components
{
	public class ProductsViewComponent : ViewComponent
	{
		private readonly ShopBearContext _context;
		public ProductsViewComponent(ShopBearContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			// Assuming categoryId is passed as a parameter when invoking the view component
			IEnumerable<Product> items = await _context.Products
					.Include(p => p.ProductImage).ToListAsync();

			return View(items);
		}
	}
}
