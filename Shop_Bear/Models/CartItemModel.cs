using Shop_Bear.Models.EF;

namespace Shop_Bear.Models
{
	public class CartItemModel
	{
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public string Alias { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
		public decimal Total
		{
			get { return Quantity * Price; }
		}
		public string Image { get; set; }
		public CartItemModel()
		{

		}
		public CartItemModel(Product product)
		{
			ProductId = product.Id;
			ProductName = product.Title;
			Alias = product.Alias;
			Price = (decimal)product.Price;
			Quantity = 1;
			Image = product.ProductImage.FirstOrDefault(x => x.IsDefault == true).Image ;
		}
	}
}
