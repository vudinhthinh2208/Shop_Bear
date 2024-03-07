using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop_Bear.Models;
using Shop_Bear.Models.EF;
using X.PagedList;

namespace Shop_Bear.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ShopBearContext _context;
        public OrderController(ShopBearContext context)
        {
            _context = context;
        }
        public ActionResult Index(int? page, string searchString)
        {
            var pageNumber = (page ?? 1);
            var pageSize = 10;

            IEnumerable<Order> query = _context.Orders.OrderByDescending(x => x.Id).AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                query = query.Where(x => x.Code.ToLower().Contains(searchString));
            }
            var items = query.ToPagedList(pageNumber, pageSize);

            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            ViewBag.SearchString = searchString;

            return View(items);
        }

        public ActionResult ViewDetail(int id)
        {
            var item = _context.Orders.Include(p => p.OrderDetails).ThenInclude(od => od.Product).FirstOrDefault(x => x.Id == id);
            return View(item);
        }
        [HttpPost]
        public ActionResult Update(int id, int trangthai)
        {
            var item = _context.Orders.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                item.DatePayment = DateTime.Now;
                item.Status = trangthai;
                _context.SaveChanges();
                return Redirect("Index");
            }
            return Redirect("Index");
        }

    }
}
