using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Product.Core.Models;

namespace Product.Core.Utils
{
    public class ProductFilter
    {
        private Dictionary<string, (int, int?)> priceLevels;

        public ProductFilter()
        {
            priceLevels = new Dictionary<string, (int, int?)>
        {
            { "Max", (500000, null) },
            { "Highest", (350000, 500000) },
            { "High", (200000, 350000) },
            { "Medium", (100000, 200000) },
            { "Low", (0, 100000) }
        };
        }

        public List<Book> ApplyFilters(List<Book> listProducts, QueryObject query)
        {
            // Search Name
            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                listProducts = listProducts.Where(n => n.Name.Contains(query.Name, StringComparison.CurrentCultureIgnoreCase)).ToList();
            }

            // Category filter
            if (!string.IsNullOrEmpty(query.Category))
            {
                listProducts = listProducts.Where(c => c.Categories.Any(cat => cat.Name == query.Category)).ToList();
            }

            // Status filter
            if (!string.IsNullOrEmpty(query.Status))
            {
                if (query.Status == "Sale")
                {
                    listProducts = listProducts.Where(s => s.Options.Any(cat => cat.Sale > 0)).ToList();
                }
                if (query.Status == "Stocking")
                {
                    listProducts = listProducts.Where(s => s.Options.Any(cat => cat.Quantity > 0)).ToList();
                }
                if (query.Status == "Inventory")
                {
                    var sixMonthsAgo = DateTime.Now.AddMonths(-6);
                    listProducts = listProducts.Where(t => t.CreateAt <= sixMonthsAgo).ToList();
                }
                if (query.Status == "SoldOut")
                {
                    listProducts = listProducts.Where(i => i.Options.Any(cat => cat.Quantity == 0)).ToList();
                }
            }

            // Price filter
            if (!string.IsNullOrEmpty(query.SortBy) && priceLevels.TryGetValue(query.SortBy, out (int, int?) value))
            {
                var (minPrice, maxPrice) = value;
                if (maxPrice.HasValue)
                {
                    listProducts = listProducts.Where(p => p.Options.Any(cat => cat.Price >= minPrice && cat.Price <= maxPrice)).ToList();
                }
                else
                {
                    listProducts = listProducts.Where(p => p.Options.Any(cat => cat.Price > minPrice)).ToList();
                }
            }

            // Pagination
            int skip = (query.Page - 1) * query.Limit;
            listProducts = listProducts.OrderBy(p => p.CreateAt).Skip(skip).Take(query.Limit).ToList();

            return listProducts;
        }
    }

}