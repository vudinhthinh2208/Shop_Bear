using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop_Bear.Models;

namespace Shop_Bear.Repository.Components
{
	public class ProductCategoriessViewComponent : ViewComponent
	{
		private readonly ShopBearContext _context;
		public ProductCategoriessViewComponent(ShopBearContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var productCategories = await _context.ProductCategories.Take(3).ToListAsync();
			return View(productCategories);
		}

    }
}
