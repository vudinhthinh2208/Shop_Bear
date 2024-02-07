﻿using NuGet.Configuration;

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
	public class ShoppingCartItem
	{
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public string Alias {  get; set; }
		public string CategoryName { get; set; }
		public string ProductImg { get; set; }
		public int Quantity {  get; set; }
		public decimal Price { get; set; }
		public decimal PriceTotal { get; set;}
	}
}
