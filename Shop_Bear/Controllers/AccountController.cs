using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop_Bear.Models.EF;
using Shop_Bear.Models.ViewModels;

namespace Shop_Bear.Controllers
{
	public class AccountController : Controller
	{
		private UserManager<AppUser> _userManage;
		private SignInManager<AppUser> _singInManage;
		public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
		{
			_singInManage = signInManager;
			_userManage = userManager;
		}
		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				//login
				var result = await _singInManage.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
				if (result.Succeeded)
				{
					return RedirectToAction("Index", "Home");
				}
				ModelState.AddModelError("", "Invalid login attempt");
				return View(model);
			}
			return View(model);
		}
		public IActionResult LoginCart()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> LoginCart(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				//login
				var result = await _singInManage.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
				if (result.Succeeded)
				{
					return RedirectToAction("Checkout", "Checkout");
				}
				ModelState.AddModelError("", "Invalid login attempt");
				return View(model);
			}
			return View(model);
		}
		public IActionResult Register()
		{
			return View();
		}
		
		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				AppUser user = new()
				{
					UserName = model.UserName,
					Email = model.Email,
				};
				var result = await _userManage.CreateAsync(user, model.Password);
				if (result.Succeeded)
				{
					await _singInManage.SignInAsync(user, false);
					return RedirectToAction("Login", "Account");
				}
				foreach(var error in result.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}
			}

			return View(model);
		}
		public async Task<IActionResult> Logout()
		{
			await _singInManage.SignOutAsync();
			return RedirectToAction("Login", "Account");
		}
	}
}
