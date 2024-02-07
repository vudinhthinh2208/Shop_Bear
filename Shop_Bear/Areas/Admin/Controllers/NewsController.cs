using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop_Bear.Models;
using Shop_Bear.Models.EF;
using X.PagedList;

namespace Shop_Bear.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize]

	public class NewsController : Controller
    {
        private readonly ShopBearContext _context;
        public NewsController(ShopBearContext context)
        {
            _context = context;
        }
        public ActionResult Index(int? page, string searchString)
        {
            var pageNumber = (page ?? 1);
            var pageSize = 7;

            var query = _context.News.OrderByDescending(x => x.Id).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                query = query.Where(x => x.Title.ToLower().Contains(searchString));
            }

            var items = query.ToPagedList(pageNumber, pageSize);

            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            ViewBag.SearchString = searchString;

            return View(items);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(News model)
        {
            model.CreateDate = DateTime.Now;
            model.CategoryId = 21;
            model.ModifiedDate = DateTime.Now;            
            model.Alias = Shop_Bear.Models.Common.Filter.FilterChar(model.Title);
            _context.News.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            var item = _context.News.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(News model) 
        {
            model.ModifiedDate = DateTime.Now;
            model.Alias = Shop_Bear.Models.Common.Filter.FilterChar(model.Title);
            _context.Update(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = _context.News.Find(id);
            if(item != null)
            {
                _context.Remove(item);
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new {success = false});
        }
        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var item = _context.News.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                _context.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                _context.SaveChanges();
                return Json(new { success = true, isActive = item.IsActive });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult DeleteAll(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var items = ids.Split(',');
                if (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        var obj = _context.News.Find(Convert.ToInt32(item));
                        _context.News.Remove(obj);
                        _context.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}
