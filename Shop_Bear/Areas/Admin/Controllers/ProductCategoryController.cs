using Microsoft.AspNetCore.Mvc;
using Shop_Bear.Models;
using Shop_Bear.Models.EF;

namespace Shop_Bear.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductCategoryController : Controller
    {
        private readonly ShopBearContext _context;
        public ProductCategoryController(ShopBearContext context)
        {
            _context = context;
        }
        public ActionResult Index()
        {
            var items = _context.ProductCategories.ToList();
            return View(items);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ProductCategory model)
        {
            model.CreateDate = DateTime.Now;
            model.ModifiedDate = DateTime.Now;
            model.CreateBy = "h";
            model.ModifiedBy = "j";
            model.Alias = Shop_Bear.Models.Common.Filter.FilterChar(model.Title);
            _context.ProductCategories.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
