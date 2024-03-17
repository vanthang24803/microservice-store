using Product.Core.Dtos.Order;
using Product.Core.Dtos.Response;
using Product.Core.Models;

namespace Product.Core.Interfaces
{
    public interface IOrderService
    {
        Task<IResponse> CreateAsync(OrderDto order);

        Task<IResponse> UpdateOrderAsync(string id, UpdateOrderDto updateDto);

        Task<List<Order>> FindAllAsync();
        Task<List<Order>> FindUserOrderAsync(string id);

        Task<Order?> FindByIdAsync(string id);
        Task<IResponse> DeleteAsync(string id);

    }
}