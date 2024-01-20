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
		public async Task<IViewComponentResult> InvokeAsync() => View(await _context.ProductCategories.ToListAsync());
	}
}
