using NuGet.Configuration;

namespace Shop_Bear.Models
{
	public class ShoppingCart
	{
		public List<ShoppingCartItem> Items { get; set; }
		public ShoppingCart()
		{
			this.Items = new List<ShoppingCartItem>();
		}
		public void AddToCart(ShoppingCartItem item, int Quantity)
		{
			var checkExits = Items.FirstOrDefault(x => x.ProductId == item.ProductId);
			if(checkExits != null)
			{
				checkExits.Quantity += Quantity;
				checkExits.PriceTotal = checkExits.Price * checkExits.Quantity;
			}
			else
			{
				Items.Add(item);
			}
		}
		public void Remove(int id)
		{
			var checkExits = Items.SingleOrDefault(x => x.ProductId == id);
			if(checkExits != null)
			{
				Items.Remove(checkExits);
			}
		}
		public void UpdateQuantity(int id, int quantity)
		{
			var checkExits = Items.SingleOrDefault(x => x.ProductId == id);
			if (checkExits != null)
			{
				checkExits.Quantity = quantity;
				checkExits.PriceTotal = checkExits.Price * checkExits.Quantity;
			}
		}
		public decimal GetToTalPrice()
		{
			return Items.Sum(x => x.PriceTotal);
		}
		public decimal GetToTalQuantity()
		{
			return Items.Sum(x => x.Quantity);
		}
		public void ClearCart()
		{
			Items.Clear();
		}
	}
	public static class SessionExtensions
	{
		public static void Set(this ISession session, string key, byte[] value)
		{
			session.Set(key, value);
		}

		public static byte[] Get(this ISession session, string key)
		{
			return session.Get(key);
		}
	}
	public class ShoppingCartItem
	{
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public string CategoryName { get; set; }
		public string ProductImg { get; set; }
		public int Quantity {  get; set; }
		public decimal Price { get; set; }
		public decimal PriceTotal { get; set;}
	}
}
