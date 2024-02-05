using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Order.core.Context;
using Order.Core.Dtos;
using Order.Core.Interfaces;
using Order.core.Dtos;
using Order.core.Models;
using Order.Core.Utils;


namespace Order.Core.Services
{
    public class Statistical : IStatistical
    {
        private readonly ApplicationDbContext _context;

        public Statistical(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<StatisticalDto> CreateAsync(QueryObject query)
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
                Orders = orders.Select(Mapping).ToList(),
            };

            return result;
        }


        public OrderDto Mapping(Order.core.Models.Order order)
        {
            return new OrderDto
            {
                Id = order.Id,
                Products = order.Products,
                Email = order.Email,
                Payment = order.Payment,
                Quantity = order.Quantity,
                Shipping = order.Shipping,
                Status = order.Status,
                TotalPrice = order.TotalPrice,
                UserId = order.UserId,
                Address = order.Address,
                CreateAt = order.CreateAt,
                NumberPhone = order.NumberPhone,
                Name = order.Name,
            };
        }

    }
}