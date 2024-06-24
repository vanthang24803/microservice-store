
using Product.Core.Domain.Dtos.Billboard;
using Product.Core.Models;

namespace Product.Core.Repositories
{
    public interface IBillboardRepository
    {
        Task<Billboard> Save(BillboardDto createBillboard, IFormFile file);

        Task<Billboard> Update(Guid id, BillboardDto updateBillboard, IFormFile file);

        Task<List<Billboard>> FindAll();

        Task<Billboard> FindDetail(Guid id);

        Task<string> Delete(Guid id);
    }
}