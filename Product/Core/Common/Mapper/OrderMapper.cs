using Product.Core.Dtos.Order;
using Product.Core.Models;
namespace Product.Core.Mapper
{

    public class OrderMapper
    {
        public static Order MapToOrder(OrderDto orderDto)
        {
            return new Order
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
                NumberPhone = orderDto.NumberPhone,
                Name = orderDto.Name,
                CreateAt = orderDto.CreateAt,
            };
        }

        public static OrderDto MapFromDto(Order order)
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
                NumberPhone = order.NumberPhone,
                Name = order.Name,
                CreateAt = order.CreateAt
            };
        }
    }
}