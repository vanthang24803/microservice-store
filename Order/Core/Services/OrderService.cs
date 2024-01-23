using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Order.core.Context;
using Order.core.Dtos;
using Order.core.Enum;
using Order.core.Interfaces;
using Order.Core.Dtos;

namespace Order.core.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response> CreateAsync(string userId, OrderDto orderDto)
        {
            var order = MapToOrder(orderDto, userId);
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            return new Response()
            {
                IsSucceed = true,
                Message = "Order created successfully"
            };
        }


        public async Task<Response> DeleteAsync(Guid id)
        {
            var exitingOrder = await _context.Orders.FindAsync(id);

            if (exitingOrder is null)
            {
                return new Response()
                {
                    IsSucceed = false,
                    Message = "Order not found"
                };
            }

            _context.Orders.Remove(exitingOrder);
            await _context.SaveChangesAsync();

            return new Response()
            {
                IsSucceed = true,
                Message = "Order deleted success"
            };
        }

        public async Task<List<Models.Order>> GetAsync(string UserId)
        {
            var result = await _context.Orders.Where(c => c.UserId == UserId).Include(c => c.Products).ToListAsync();

            return result;
        }


        public async Task<List<Models.Order>> GetAllAsync()
        {
            return await _context.Orders.Include(p => p.Products).ToListAsync();
        }

        public async Task<Response> UpdateAsync(Guid id, UpdateDto updateDto)
        {
            var exitingOrder = await _context.Orders.FindAsync(id);

            if (exitingOrder is null)
            {
                return new Response()
                {
                    IsSucceed = false,
                    Message = "Order not found"
                };
            }

            exitingOrder.Status = updateDto.Status;

            await _context.SaveChangesAsync();

            return new Response()
            {
                IsSucceed = true,
                Message = "Order status updated success"
            };

        }


        public Models.Order MapToOrder(OrderDto orderDto, string userId)
        {
            return new Models.Order
            {
                Id = orderDto.Id,
                Products = orderDto.Products,
                Email = orderDto.Email,
                Payment = orderDto.Payment,
                Quantity = orderDto.Quantity,
                Shipping = orderDto.Shipping,
                Status = orderDto.Status,
                TotalPrice = orderDto.TotalPrice,
                Voucher = orderDto.Voucher,
                UserId = userId,
                Address = orderDto.Address,
                CreateAt = orderDto.CreateAt,
                NumberPhone = orderDto.NumberPhone,
            };
        }


    }
}