using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop_Bear.Models;

namespace Shop_Bear.Controllers
{
    public class NewsController : Controller
    {
        private readonly ShopBearContext _db;
        public NewsController(ShopBearContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var items = _db.News.ToList();
            return View(items);
        }
        public async Task<IActionResult> Detail(string alias)
        {
            if (alias == null)
            {
                return RedirectToAction("Index");
            }
            var newsByAlias = _db.News
                    .Where(x => x.Alias == alias)
                    .FirstOrDefault();
            
            return View(newsByAlias);
        }
    }
}
