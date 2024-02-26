using Microsoft.AspNetCore.Mvc;
using Shop_Bear.Models;

namespace Shop_Bear.Controllers
{
    public class SalesController : Controller
    {
        private readonly ShopBearContext _context;
        public SalesController(ShopBearContext context)
        {
            _context = context;
        }
        public IActionResult Index(string alias)
        {
            var item = _context.Posts.FirstOrDefault(x => x.Alias == alias);
            return View();
        }
    }
}
