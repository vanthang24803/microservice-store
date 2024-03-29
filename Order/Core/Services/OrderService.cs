using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Order.core.Context;
using Order.core.Dtos;
using Order.core.Enum;
using Order.core.Interfaces;
using Order.Core.Dtos;
using Order.Core.Interfaces;
using Order.Core.Utils;

namespace Order.core.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        private readonly IGmailService _gmailService;


        public OrderService(ApplicationDbContext context, IGmailService gmailService)
        {
            _context = context;
            _gmailService = gmailService;
        }

        public async Task<Response> CreateAsync(OrderDto orderDto)
        {
            var order = MapToOrder(orderDto);
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            try
            {
                MailRequest mailRequest = new()
                {
                    ToEmail = orderDto.Email,
                    Subject = "Xác nhận đơn hàng",
                    Message = MailSend.OrderMailSend(order)
                };
                await _gmailService.SendEmailAsync(mailRequest);

                return new Response()
                {
                    IsSucceed = true,
                    Message = "Order created successfully"
                };

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new Response()
                {
                    IsSucceed = false,
                    Message = "Mail Send Wrong!"
                };
            }
        }


        public async Task<Response> DeleteAsync(string id)
        {
            var existingOrder = await _context.Orders.Include(o => o.Products).SingleOrDefaultAsync(o => o.Id == id);

            if (existingOrder is null)
            {
                return new Response()
                {
                    IsSucceed = false,
                    Message = "Order not found"
                };
            }

            _context.Products.RemoveRange(existingOrder.Products);
            _context.Orders.Remove(existingOrder);
            await _context.SaveChangesAsync();

            return new Response()
            {
                IsSucceed = true,
                Message = "Order and associated products deleted successfully"
            };
        }

        public async Task<List<Models.Order>> GetAsync(string UserId)
        {
            var result = await _context.Orders.Where(c => c.UserId == UserId).Include(c => c.Products).ToListAsync();


            return [.. result.OrderByDescending(c => c.CreateAt)];
        }


        public async Task<List<Models.Order>> GetAllAsync()
        {
            var result = await _context.Orders.Include(p => p.Products).ToListAsync();

            return [.. result.OrderByDescending(c => c.CreateAt)];
        }

        public async Task<Response> UpdateAsync(string id, UpdateDto updateDto)
        {
            var exitingOrder = await _context.Orders
                .Include(o => o.Products)
                .SingleOrDefaultAsync(o => o.Id == id);

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


            try
            {
                MailRequest mailRequest = new()
                {
                    ToEmail = exitingOrder.Email,
                    Subject = $"Đơn hàng của bạn đã được cập nhật",
                    Message = MailSend.OrderMailSend(exitingOrder)
                };
                await _gmailService.SendEmailAsync(mailRequest);

                return new Response()
                {
                    IsSucceed = true,
                    Message = "Order status updated success"
                };

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new Response()
                {
                    IsSucceed = false,
                    Message = "Mail Send Wrong!"
                };
            }
        }


        public Models.Order MapToOrder(OrderDto orderDto)
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
                UserId = orderDto.UserId,
                Address = orderDto.Address,
                CreateAt = orderDto.CreateAt,
                NumberPhone = orderDto.NumberPhone,
                Name = orderDto.Name,
            };
        }

        public async Task<Models.Order?> GetDetailAsync(string id)
        {
            var result = await _context.Orders.Include(p => p.Products).FirstOrDefaultAsync(p => p.Id == id);

            if (result is null)
            {
                return null;
            }

            return result;
        }

    }

}