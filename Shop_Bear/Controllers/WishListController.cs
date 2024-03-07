using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop_Bear.Models;
using Shop_Bear.Models.EF;

namespace Shop_Bear.Controllers
{
	public class WishListController : Controller
	{
		private readonly ShopBearContext _context;
        public WishListController(ShopBearContext context)
        {
            _context = context;
        }
        public IActionResult Index()
		{
			if (!User.Identity.IsAuthenticated)
			{
				// Redirect to login page or return unauthorized response
				return RedirectToAction("Login", "Account");
			}

			var userId = User.Identity.Name;

			// Lấy các sản phẩm yêu thích của người dùng hiện tại
			var userWishList = _context.WishLists
									.Where(w => w.UserName == userId)
									.Include(w => w.Product).Where(x => x.Product.IsActive == true)
									.Include(w => w.Product.ProductImage)
									.ToList();

			return View(userWishList);
		}
		[HttpPost]
		public IActionResult PostWishList(int id)
		{
			if (!User.Identity.IsAuthenticated)
			{
				// Redirect to login page or return unauthorized response
				return RedirectToAction("Login", "Account");
			}

			var userId = User.Identity.Name;

			// Kiểm tra xem sản phẩm đã tồn tại trong danh sách yêu thích của người dùng hay chưa
			var existingWishListItem = _context.WishLists
											.FirstOrDefault(w => w.ProductId == id && w.UserName == userId);

			if (existingWishListItem != null)
			{
				// Nếu sản phẩm đã tồn tại, bạn có thể xử lý thông báo hoặc chuyển hướng đến trang yêu thích
				return RedirectToAction("Index", "Home");
			}

			// Nếu sản phẩm chưa tồn tại, thêm sản phẩm vào danh sách yêu thích của người dùng
			var item = new WishList
			{
				ProductId = id,
				UserName = userId,
				CreatedDate = DateTime.Now
			};

			_context.WishLists.Add(item);
			_context.SaveChanges();

			// Chuyển hướng đến trang yêu thích sau khi thêm sản phẩm thành công
			return RedirectToAction("Index", "Home");
		}
		[HttpPost]
		public IActionResult RemoveWishList(int id)
		{
			if (!User.Identity.IsAuthenticated)
			{
				// Trả về trang đăng nhập để thực hiện lấy IdUser
				return RedirectToAction("Login", "Account");
			}

			var userId = User.Identity.Name;

			// Tìm sản phẩm yêu thích cần xóa
			var wishListItem = _context.WishLists.FirstOrDefault(w => w.ProductId == id && w.UserName == userId);

			if (wishListItem == null)
			{
				// Nếu không tìm thấy sản phẩm yêu thích, có thể xử lý thông báo hoặc chuyển hướng đến trang yêu thích
				return RedirectToAction("Index", "WishList");
			}

			// Xóa sản phẩm yêu thích
			_context.WishLists.Remove(wishListItem);
			_context.SaveChanges();

			// Chuyển hướng đến trang yêu thích sau khi xóa thành công
			return RedirectToAction("Index", "WishList");
		}

	}
}
