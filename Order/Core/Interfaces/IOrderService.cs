using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Order.core.Dtos;
using Order.Core.Dtos;

namespace Order.core.Interfaces
{
    public interface IOrderService
    {
        Task<Response> CreateAsync(string userId, OrderDto order);

        Task<Response> UpdateAsync(Guid id, UpdateDto updateDto);

        Task<List<Models.Order>> GetAsync(string UserId);
        Task<List<Models.Order>> GetAllAsync();

        Task<Response> DeleteAsync(Guid id);
    }
}