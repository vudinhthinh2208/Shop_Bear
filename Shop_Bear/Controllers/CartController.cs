using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop_Bear.Models;
using Shop_Bear.Models.EF;
using Shop_Bear.Models.ViewModels;
using Shop_Bear.Repository;

namespace Shop_Bear.Controllers
{
	public class CartController : Controller
	{
		private readonly ShopBearContext _db;
        public CartController(ShopBearContext db)
        {
            _db = db;
        }
        public IActionResult Index()
		{
			List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
			CartItemViewModel cartVM = new()
			{
				CartItems = cartItems,
				GrandTotal = cartItems.Sum(x => x.Quantity * x.Price)
			};
			return View(cartVM);
		}
		
		public async Task<IActionResult> Add(int id)
		{
			var product = await _db.Products.Include(x => x.ProductImage).FirstOrDefaultAsync(x => x.Id == id);

			if (product == null)
			{
				// Nếu không tìm thấy sản phẩm thì trả về giao diện của giỏ hàng
				TempData["error"] = "Product not found.";
				return RedirectToAction("Index");
			}

			List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
			CartItemModel cartItem = cart.FirstOrDefault(item => item.ProductId == id);

			if (cartItem == null)
			{
				// Nếu sản phẩm chưa có trong giỏ hàng thì thêm mới nó
				cart.Add(new CartItemModel(product));
			}
			else
			{
				// Nếu sản phẩm có trong giỏ hàng thì tăng số lượng sản phẩm đó lên
				cartItem.Quantity += 1;
			}

			HttpContext.Session.SetJson("Cart", cart);
			TempData["success"] = "Item added to cart successfully."; 
			return Redirect(Request.Headers["Referer"].ToString());
		}
		//Giảm số lượng
		public async Task<IActionResult> Decrease(int Id)
		{
			List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");
			CartItemModel cartItem = cart.Where(_ => _.ProductId == Id).FirstOrDefault();
			if (cartItem.Quantity > 1)
			{
				--cartItem.Quantity;
			}
			else
			{
				cart.RemoveAll(_ => _.ProductId == Id);
			}
			if (cart.Count == 0)
			{
				HttpContext.Session.Remove("Cart");
			}
			else
			{
				HttpContext.Session.SetJson("Cart", cart);
			}
			TempData["success"] = "Decrease Item quantity to cart Succesfully";
			return RedirectToAction("Index");
		}
		//Tăng số lượng
		public async Task<IActionResult> Increase(int Id)
		{
			List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");
			CartItemModel cartItem = cart.Where(_ => _.ProductId == Id).FirstOrDefault();
			if (cartItem.Quantity >= 1)
			{
				++cartItem.Quantity;
			}
			else
			{
				cart.RemoveAll(_ => _.ProductId == Id);
			}
			if (cart.Count == 0)
			{
				HttpContext.Session.Remove("Cart");
			}
			else
			{
				HttpContext.Session.SetJson("Cart", cart);
			}
			TempData["success"] = "Increase Item quantity to cart Succesfully";
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> Remove(int Id)
		{
			List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");
			cart.RemoveAll(p => p.ProductId == Id);
			if (cart.Count == 0)
			{
				HttpContext.Session.Remove("Cart");
			}
			else
			{
				HttpContext.Session.SetJson("Cart", cart);
			}
			TempData["success"] = "Remove Item of cart Succesfully";
			return RedirectToAction("Index");
		}
		public async Task<IActionResult> Clear(int Id)
		{
			HttpContext.Session.Remove("Cart");
			TempData["success"] = "Clear Item of cart Succesfully";
			return RedirectToAction("Index");
		}
	}
}
