using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Order.core.Dtos;
using Order.Core.Dtos;

namespace Order.core.Interfaces
{
    public interface IOrderService
    {
        Task<Response> CreateAsync(OrderDto order);

        Task<Response> UpdateAsync(string id, UpdateDto updateDto);

        Task<List<Models.Order>> GetAsync(string id);
        Task<List<Models.Order>> GetAllAsync();

        Task<Models.Order> GetDetailAsync(string id);

        Task<Response> DeleteAsync(string id);
    }
}