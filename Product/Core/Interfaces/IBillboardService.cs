using Product.Core.Common.Utils;
using Product.Core.Domain.Dtos.Billboard;
using Product.Core.Models;

namespace Product.Core.Interfaces
{
    public interface IBillboardService
    {
        Task<Response<Billboard>> CreateAsync(BillboardDto createBillboard, IFormFile file);

        Task<Response<Billboard>> UpdateAsync(Guid id, BillboardDto updateBillboard, IFormFile file);

        Task<List<Billboard>> GetAsync();

        Task<Billboard> GetDetailAsync(Guid id);

        Task<string> DeleteAsync(Guid id);
    }
}