using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shop_Bear.Models;
using Shop_Bear.Models.EF;
using X.PagedList;

namespace Shop_Bear.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly ShopBearContext _context;
        public ProductsController(ShopBearContext context)
        {
            _context = context;
        }
        public ActionResult Index(int? page, string searchString)
        {
            var pageNumber = (page ?? 1);
            var pageSize = 7;

			IEnumerable<Product> query = _context.Products
					.Include(p => p.ProductImage)
                    .Include(p => p.ProductCategory)
					.OrderByDescending(x => x.Id)
					.AsQueryable();
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
            ViewBag.ProductCategory = new SelectList(_context.ProductCategories.ToList(), "Id", "Title");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Product model, List<string> Images, List<int> rDefault)
        {
            if (Images != null && Images.Count > 0)
            {
                for (int i = 0; i < Images.Count; i++)
                {
                    if (i + 1 == rDefault[0])
                    {
                        model.Image = Images[i];
                        model.ProductImage.Add(new ProductImage
                        {
                            ProductId = model.Id,
                            Image = Images[i],
                            IsDefault = true
                        });
                    }
                    else
                    {
                        model.ProductImage.Add(new ProductImage
                        {
                            ProductId = model.Id,
                            Image = Images[i],
                            IsDefault = false
                        });
                    }
                }
            }
			model.CreateDate = DateTime.Now;
            model.ModifiedDate = DateTime.Now;
            model.Alias = Shop_Bear.Models.Common.Filter.FilterChar(model.Title);
            _context.Products.Add(model);
            _context.SaveChanges();
            ViewBag.ProductCategory = new SelectList(_context.ProductCategories.ToList(), "Id", "Title");
            return RedirectToAction("Index");
        }
		public ActionResult Edit(int id)
		{
			ViewBag.ProductCategory = new SelectList(_context.ProductCategories.ToList(), "Id", "Title");
            var item = _context.Products.Find(id);
            return View(item);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Product model)
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
            var item = _context.Products.Find(id);
            if (item != null)
            {
                _context.Remove(item);
                _context.SaveChanges();
                return Json(new { success = true });
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
						var obj = _context.Products.Find(Convert.ToInt32(item));
						_context.Products.Remove(obj);
						_context.SaveChanges();
					}
				}
				return Json(new { success = true });
			}
			return Json(new { success = false });
		}
	}
}
