using Microsoft.EntityFrameworkCore;
using Product.Core.Models;
using Product.Migrations;

namespace Product.Core.Utils
{
    public class OrderUserFilter
    {
        public List<UserTotalPrice> ApplyFilters(List<Order> orders, QueryObjectOrder query)
        {
            DateTime today = DateTime.Today;
            int currentMonth = DateTime.Now.Month;
            int currentYear = DateTime.Now.Year;

            if (!string.IsNullOrEmpty(query.Time) && query.Time == "Day")
            {
                orders = orders.Where(o => o.CreateAt.Date == today).ToList();
            }
            else if (!string.IsNullOrEmpty(query.Time) && query.Time == "Month")
            {
                orders = orders.Where(o => o.CreateAt.Month == currentMonth).ToList();
            }

            else if (!string.IsNullOrEmpty(query.Time) && query.Time == "Year")
            {
                orders = orders.Where(o => o.CreateAt.Year == currentYear).ToList();
            }

            List<UserTotalPrice> userTotalPriceList = [.. orders.GroupBy(o => o.UserId)
                                                            .Select(g => new UserTotalPrice { UserId = g.Key, TotalPrice = g.Sum(o => o.TotalPrice), TotalOrder = g.Count() })
                                                            .OrderByDescending(u => u.TotalPrice)];

            return userTotalPriceList;
        }

        public class UserTotalPrice
        {
            public string UserId { get; set; } = string.Empty;
            public double TotalPrice { get; set; }
            public int TotalOrder { get; set; }
        }
    }
}
