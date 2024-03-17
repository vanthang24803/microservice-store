using Product.Context;
using Product.Core.Dtos.Order;
using Product.Core.Interfaces;
using Product.Core.Utils;
using Microsoft.EntityFrameworkCore;
using Product.Core.Mapper;


namespace Product.Core.Services
{
    public class Statistical : IStatistical
    {
        private readonly ApplicationDbContext _context;

        public Statistical(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<StatisticalDto> CreateAsync(QueryObjectOrder query)
        {
            var ordersQuery = _context.Orders.AsQueryable();

            var filter = new StatisticalFilter();

            ordersQuery = filter.ApplyFilters(ordersQuery, query);

            var totalOrder = await ordersQuery.CountAsync();

            var totalProduct = await ordersQuery.SelectMany(p => p.Products).SumAsync(p => p.Quantity);
            var totalPrice = await ordersQuery.SumAsync(p => p.TotalPrice);

            var orders = await ordersQuery.Include(c => c.Products).ToListAsync();

            var result = new StatisticalDto()
            {
                TotalOrder = totalOrder,
                TotalPrice = totalPrice,
                TotalProduct = totalProduct,
                Orders = orders.Select(OrderMapper.MapFromDto).ToList(),
            };

            return result;
        }

    }
}