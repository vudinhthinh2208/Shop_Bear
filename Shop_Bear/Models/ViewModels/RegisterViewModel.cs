using System.ComponentModel.DataAnnotations;

namespace Shop_Bear.Models.ViewModels
{
	public class RegisterViewModel
	{
		[Required(ErrorMessage = "User không được để trống")]
		public string? UserName { get; set; }
		[DataType(DataType.EmailAddress)]
		public string? Email {  get; set; }
		[DataType(DataType.Password), Required(ErrorMessage = "Vui lòng nhập passwword")]
		public string? Password { get; set; }
		[Compare("Password", ErrorMessage = "Password don't match")]
		[Display(Name = "Confirm Password")]
		[DataType(DataType.Password)]
		public string? ConfirmPassword { get; set; }
	}
}
