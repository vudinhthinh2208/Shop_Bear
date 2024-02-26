using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop_Bear.Models;

namespace Shop_Bear.Repository.Components
{
    public class NewsViewComponent : ViewComponent
    {
        private readonly ShopBearContext _context;
        public NewsViewComponent(ShopBearContext context)
        {
            _context = context;
        }
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var news = await _context.News.Take(3).ToListAsync();
			return View(news);
		}
	}
}
