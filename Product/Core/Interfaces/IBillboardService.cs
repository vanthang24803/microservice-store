using Product.Core.Dtos.Billboard;
using Product.Core.Models;

namespace Product.Core.Interfaces
{
    public interface IBillboardService
    {
        Task<ResponseDto> CreateAsync(CreateBillboard createBillboard, IFormFile file);

        Task<ResponseDto> UpdateAsync(Guid id, UpdateBillboard updateBillboard, IFormFile file);

        Task<List<Billboard>> GetAsync();

        Task<Billboard?> GetDetailAsync(Guid id);

        Task<ResponseDto> DeleteAsync(Guid id);
    }
}