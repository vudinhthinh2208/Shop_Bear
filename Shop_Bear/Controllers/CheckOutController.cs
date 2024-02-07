using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop_Bear.Models;
using Shop_Bear.Models.EF;
using Shop_Bear.Repository;
using System.Security.Claims;

namespace Shop_Bear.Controllers
{
	[Authorize]
	public class CheckOutController : Controller
	{
		
		private readonly ShopBearContext _context;
        public CheckOutController(ShopBearContext context)
        {
			_context = context;
        }
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult CheckOut()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> CheckOut(Order orderItem, int TypePayment)
		{
			
				if(ModelState.IsValid)
				{
					List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
					var ordercode = Guid.NewGuid().ToString();
					orderItem.Code = ordercode;
					orderItem.Status = 1;
					orderItem.CreateDate = DateTime.Now;
					orderItem.TypePayment = TypePayment;
					orderItem.TotalAmount = cartItems.Sum(x => x.Quantity * x.Price);
					_context.Add(orderItem);
					await _context.SaveChangesAsync();
				foreach (var cart in cartItems)
				{
					var orderDetails = new OrderDetail();
					orderDetails.OrderId = orderItem.Id;
					orderDetails.ProductId = cart.ProductId;
					orderDetails.Price = cart.Price;
					orderDetails.Quantity =	cart.Quantity;
					_context.Add(orderDetails);
					await _context.SaveChangesAsync();
				}
					HttpContext.Session.Remove("Cart");
					TempData["success"] = "Đơn hàng đã được tạo. Vui lòng chờ duyệt đơn hàng";
					return RedirectToAction("Index", "Cart");
					
				}
				return View(orderItem);
			
		}
	}
}
