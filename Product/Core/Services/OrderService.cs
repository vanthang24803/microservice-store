using Microsoft.EntityFrameworkCore;
using Product.Context;
using Product.Core.Dtos.Order;
using Product.Core.Dtos.Response;
using Product.Core.Interfaces;
using Product.Core.Mapper;
using Product.Core.Models;
using Product.Core.Utils;

namespace Product.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        private readonly IMailService _mailService;


        public OrderService(ApplicationDbContext context, IMailService mailService)
        {
            _context = context;
            _mailService = mailService;
        }


        public async Task<IResponse> CreateAsync(OrderDto orderDto)
        {
            var order = OrderMapper.MapToOrder(orderDto);

            _context.Orders.Add(order);

            foreach (var product in orderDto.Products)
            {
                var book = await _context.Books.FindAsync(Guid.Parse(product.ProductId));

                if (book is null)
                {
                    return new ResponseDto()
                    {
                        IsSucceed = false,
                        Message = "Book not found"
                    };
                }

                var option = await _context.Options.FindAsync(Guid.Parse(product.OptionId));

                if (option is null)
                {
                    return new ResponseDto()
                    {
                        IsSucceed = false,
                        Message = "Option not found"
                    };
                }

                option.Quantity -= product.Quantity;
            }

            try
            {
                MailRequest mailRequest = new()
                {
                    ToEmail = orderDto.Email,
                    Subject = "Xác nhận đơn hàng",
                    Message = MailSend.OrderMailSend(order)
                };
                await _mailService.SendEmailAsync(mailRequest);

                await _context.SaveChangesAsync();

                return new ResponseDto()
                {
                    IsSucceed = true,
                    Message = "Order created successfully"
                };

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Mail Send Wrong!"
                };
            }


        }

        public async Task<IResponse> DeleteAsync(string id)
        {
            var existingOrder = await _context.Orders.Include(o => o.Products).SingleOrDefaultAsync(o => o.Id == id);

            if (existingOrder is null)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Order not found"
                };
            }

            _context.OrderDetails.RemoveRange(existingOrder.Products);
            _context.Orders.Remove(existingOrder);
            await _context.SaveChangesAsync();

            return new ResponseDto()
            {
                IsSucceed = true,
                Message = "Order and associated products deleted successfully"
            };
        }

        public async Task<List<Order>> FindAllAsync()
        {
            var result = await _context.Orders.Include(p => p.Products).ToListAsync();

            return [.. result.OrderByDescending(c => c.CreateAt)];
        }

        public async Task<Order?> FindByIdAsync(string id)
        {
            var result = await _context.Orders.Include(p => p.Products).FirstOrDefaultAsync(p => p.Id == id);

            if (result is null)
            {
                return null;
            }

            return result;
        }

        public async Task<List<Order>> FindUserOrderAsync(string userId)
        {
            var result = await _context.Orders.Where(c => c.UserId == userId).Include(c => c.Products).ToListAsync();


            return [.. result.OrderByDescending(c => c.CreateAt)];
        }

        public async Task<IResponse> UpdateOrderAsync(string id, UpdateOrderDto updateDto)
        {
            var exitingOrder = await _context.Orders
                .Include(o => o.Products)
                .SingleOrDefaultAsync(o => o.Id == id);

            if (exitingOrder is null)
            {
                return new ResponseDto()
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
                await _mailService.SendEmailAsync(mailRequest);

                return new ResponseDto()
                {
                    IsSucceed = true,
                    Message = "Order status updated success"
                };

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Mail Send Wrong!"
                };
            }
        }
    }
}