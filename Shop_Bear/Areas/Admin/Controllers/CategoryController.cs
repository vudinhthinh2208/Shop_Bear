using Microsoft.AspNetCore.Mvc;
using Shop_Bear.Models;
using Shop_Bear.Models.EF;

namespace Shop_Bear.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ShopBearContext _context;
        public CategoryController(ShopBearContext context)
        {
               _context = context;
        }
        public ActionResult Index()
        {
            var items = _context.Categories;
            return View(items);
        }
        public ActionResult Add() 
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Category model)
        {
                model.CreateDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.Alias = Shop_Bear.Models.Common.Filter.FilterChar(model.Title);
                _context.Categories.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            var item = _context.Categories.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category model)
        {
            _context.Attach(model);
            model.ModifiedDate = DateTime.Now;
            model.Alias = Shop_Bear.Models.Common.Filter.FilterChar(model.Title);
            _context.Entry(model).Property(x => x.Title).IsModified = true;
            _context.Entry(model).Property(x => x.Description).IsModified = true;
            _context.Entry(model).Property(x => x.Alias).IsModified = true;
            _context.Entry(model).Property(x => x.SeoDescription).IsModified = true;
            _context.Entry(model).Property(x => x.SeoKeywords).IsModified = true;
            _context.Entry(model).Property(x => x.SeoTitle).IsModified = true;
            _context.Entry(model).Property(x => x.Position).IsModified = true;
            _context.Entry(model).Property(x => x.ModifiedDate).IsModified = true;
            _context.Entry(model).Property(x => x.Position).IsModified = true;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = _context.Categories.Find(id);
            if (item != null)
            {
                //var DeleteItem = _context.Categories.Attach(item);
                _context.Categories.Remove(item);
                _context.SaveChanges();
                return Json(new {success = true});
            }
            return Json(new { success = false });
        }
    }
}
