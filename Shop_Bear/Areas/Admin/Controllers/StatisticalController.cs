using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shop_Bear.Models;
using Shop_Bear.Models.ViewModels;

namespace Shop_Bear.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize]
	public class StatisticalController : Controller
    {
        private readonly ShopBearContext _context;
        public StatisticalController(ShopBearContext context)
        {
            _context = context;
        }
		public IActionResult Index()
		{
			var statisticalData = GetStatisticalData(); // Phương thức này lấy dữ liệu thống kê từ cơ sở dữ liệu
			return View(statisticalData);
		}
		public IActionResult Yearly(int year)
		{
			var yearlyStatisticalData = GetYearlyStatisticalData(year);
			return View( yearlyStatisticalData);
		}

		private List<StatisticalViewModel> GetStatisticalData()
        {
            var query = from o in _context.Orders
                        join od in _context.OrderDetails
                        on o.Id equals od.OrderId
                        join p in _context.Products
                        on od.ProductId equals p.Id
                        where o.Status == 1 || o.Status == 2
                        select new
                        {
                            CreatedDate = o.CreateDate,
                            Quantity = od.Quantity,
                            Price = od.Price,
                            OriginalPrice = p.OriginalPrice,
							PriceSale = p.PriceSale
                        };

            var result = query.GroupBy(x => x.CreatedDate.Date).Select(x => new
            {
                Date = x.Key,
                TotalBuy = x.Sum(y => y.Quantity * y.PriceSale),
                TotalSell = x.Sum(y => y.Quantity * y.OriginalPrice),
            }).Select(x => new StatisticalViewModel
            {
                Date = x.Date,
                DoanhThu = (decimal)x.TotalBuy,
                LoiNhuan = (decimal)x.TotalBuy - (decimal)x.TotalSell
			}).ToList();

            return result;
        }
		private List<StatisticalViewModel> GetYearlyStatisticalData(int year)
		{
			var query = from o in _context.Orders
						join od in _context.OrderDetails on o.Id equals od.OrderId
						join p in _context.Products on od.ProductId equals p.Id
						where o.CreateDate.Year == year && (o.Status == 1 || o.Status == 2)
                        select new
						{
							CreatedDate = o.CreateDate,
							Quantities = od.Quantity,
							Prices = od.Price,
                            PriceSale = p.PriceSale,

                            OriginalPrices = p.OriginalPrice
						};

			var result = query.GroupBy(x => x.CreatedDate.Month).Select(x => new
			{
				Month = x.Key,
				TotalBuy = x.Sum(y => y.Quantities * y.PriceSale),
				TotalSell = x.Sum(y => y.Quantities * y.OriginalPrices),
			}).Select(x => new StatisticalViewModel
			{
				Date = new DateTime(year, x.Month, 1),
				DoanhThu = x.TotalBuy.GetValueOrDefault(),
				LoiNhuan = x.TotalBuy.GetValueOrDefault() - x.TotalSell.GetValueOrDefault() 
            }).ToList();

			return result;
		}

	}
}
