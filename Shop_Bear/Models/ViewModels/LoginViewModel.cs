using System.ComponentModel.DataAnnotations;

namespace Shop_Bear.Models.ViewModels
{
	public class LoginViewModel
	{
		
		[Required(ErrorMessage = "User không được để trống")]
		public string? UserName { get; set; }
		[DataType(DataType.Password), Required(ErrorMessage = "Vui lòng nhập passwword")]
		public string? Password { get; set; }
		public string? ReturnUrl { get; set; }
		[Display(Name = "Remember Me")]
		public bool RememberMe { get; set;}
	}
}
