using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop_Bear.Models;
using Shop_Bear.Repository;
using System.Text;

namespace Shop_Bear.Controllers
{
	public class ShoppingCartController : Controller
	{
		private readonly ShopBearContext _context;
        public ShoppingCartController(ShopBearContext context)
        {
            _context = context;
        }
        public IActionResult Index()
		{
			ShoppingCart cart = HttpContext.Session.GetJson<ShoppingCart>("Cart") ?? new ShoppingCart();
			if(cart != null)
			{
				return View(cart.Items);
			}
			return View();
		}
		public ActionResult ShowCount()
		{
			ShoppingCart cart = HttpContext.Session.GetJson<ShoppingCart>("Cart") ?? new ShoppingCart();
			if (cart.Items != null && cart.Items.Any())
			{
				return Json(new { Count = cart.Items.Sum(item => item.Quantity) });
			}

			return Json(new { Count = 0 });
		}
		[HttpPost]
		public ActionResult AddToCart(int id, int quantity)
		{
			var code = new { Success = false, msg = "", code = -1, Count = 0 };

			// Kiểm tra session để đảm bảo nó không null
			ShoppingCart cart = HttpContext.Session.GetJson<ShoppingCart>("Cart") ?? new ShoppingCart();

			var checkProduct = _context.Products
										.Include(p => p.ProductCategory)  // Đảm bảo làm include ProductCategory để tránh lỗi lazy loading
										.Include(p => p.ProductImage)      // Đảm bảo làm include ProductImage để tránh lỗi lazy loading
										.FirstOrDefault(x => x.Id == id);
			if (checkProduct != null)
			{
				ShoppingCartItem item = new ShoppingCartItem
				{
					ProductId = checkProduct.Id,
					ProductName = checkProduct.Title,
					Alias = checkProduct.Alias,
					CategoryName = checkProduct.ProductCategory?.Title,
					Quantity = quantity
				};
				var defaultImage = checkProduct.ProductImage?.FirstOrDefault(x => x.IsDefault);
				if (defaultImage != null)
				{
					item.ProductImg = defaultImage.Image;
				}

				item.Price = checkProduct.PriceSale > 0 ? (decimal)checkProduct.PriceSale : (decimal)checkProduct.Price;
				item.PriceTotal = item.Quantity * item.Price;

				cart.AddToCart(item, quantity);

				HttpContext.Session.SetJson("Cart", cart);

				code = new { Success = true, msg = "Thêm sản phẩm vào giỏ hàng thành công!", code = 1, Count = cart.Items.Count };
			}

			return Json(code);
		}
		[HttpPost]
		public ActionResult Delete(int id)
		{
			var code = new { Success = false, msg = "", code = -1, Count = 0 };

			ShoppingCart cart = HttpContext.Session.GetJson<ShoppingCart>("Cart") ?? new ShoppingCart();
			var itemToRemove = cart.Items.SingleOrDefault(x => x.ProductId == id);

			if (itemToRemove != null)
			{
				cart.Items.Remove(itemToRemove);
				HttpContext.Session.SetJson("Cart", cart);

				code = new { Success = true, msg = "Xóa sản phẩm khỏi giỏ hàng thành công!", code = 1, Count = cart.Items.Count };
			}
			else
			{
				code = new { Success = false, msg = "Sản phẩm không tồn tại trong giỏ hàng!", code = -1, Count = cart.Items.Count };
			}

			return RedirectToAction("Index","Cart");
		}

	}
}
