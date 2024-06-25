using Product.Core.Models;

namespace Product.Core.Repositories
{
    public interface IPhotoRepository
    {
        Task<List<Image>> Save(Guid productId, List<IFormFile> files);

        Task<List<Image>> GetAll(Guid productId);

        Task<string> Delete(Guid productId, Guid id);
    }
}