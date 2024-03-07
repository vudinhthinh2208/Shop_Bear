using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop_Bear.Models;
using Shop_Bear.Models.EF;
using Shop_Bear.Models.ViewModels;
using Shop_Bear.Repository;
using Shop_Bear.Services;
using System.Security.Claims;

namespace Shop_Bear.Controllers
{
	[Authorize]
	public class CheckOutController : Controller
	{
		
		private readonly ShopBearContext _context;
		private readonly IVnPayService _vpnPayService;
        public CheckOutController(ShopBearContext context, IVnPayService vnPayService)
        {
			_context = context;
			_vpnPayService = vnPayService;
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
					orderItem.TotalAmount = cartItems.Sum(x => x.Quantity * x.Price);
					if (TypePayment == 2)
					{
						var vnPayModel = new VnPaymentRequestModel
						{
							Amount = 300000,
							CreatedDate = DateTime.Now,
							OrderId = orderItem.Id,
						};
						return Redirect(_vpnPayService.CreatePaymentUrl(HttpContext, vnPayModel));
					}
					orderItem.TypePayment = TypePayment;
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
		public IActionResult PaymentFail()
		{
			return View();
		}
		public IActionResult PaymentCallBack()
		{
			var responce = _vpnPayService.PaymentExecute(Request.Query);
			if(responce == null || responce.VnPayResponseCode != "00")
			{
				TempData["Message"] = $"Lỗi thanh toán VNPAY";
				return RedirectToAction("PaymentFail");
			}
			
				TempData["success"] = "Thanh toán VNPAY thành công";
				return RedirectToAction("Index", "Cart");
		}

    }
}
