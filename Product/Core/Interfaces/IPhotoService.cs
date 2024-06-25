using Product.Core.Common.Utils;
using Product.Core.Models;

namespace Product.Core.Interfaces
{
    public interface IPhotoService
    {
        public Task<Response<List<Image>>> CreateAsync(Guid productId, List<IFormFile> files);

        public Task<List<Image>> GetAsync(Guid productId);

        public Task<string> DeleteAsync(Guid productId, Guid id);
    }
}