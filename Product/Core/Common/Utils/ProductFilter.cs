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
            { "Max", (400000, null) },
            { "Highest", (300000, 400000) },
            { "High", (200000, 300000) },
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

                if (query.Status == "Selling")
                {
                    listProducts = [.. listProducts.OrderByDescending(s => s.Sold)];
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

            if (!string.IsNullOrEmpty(query.Filter))
            {
                if (query.Filter == "Alphabet")
                {
                    listProducts = [.. listProducts.OrderBy(n => n.Name)];
                }

                if (query.Filter == "ReverseAlphabet")
                {
                    listProducts = [.. listProducts.OrderByDescending(n => n.Name)];
                }
                if (query.Filter == "HighToLow")
                {
                    listProducts = [.. listProducts.OrderByDescending(o => o.Options.Max(c => c.Price))];
                }
                if (query.Filter == "LowToHigh")
                {
                    listProducts = [.. listProducts.OrderBy(o => o.Options.Min(c => c.Price))];
                }
                if (query.Filter == "Lasted")
                {
                    listProducts = [.. listProducts.OrderByDescending(n => n.CreateAt)];
                }
                if (query.Filter == "Oldest")
                {
                    listProducts = [.. listProducts.OrderBy(n => n.CreateAt)];
                }
            }


            return listProducts;
        }
    }

}