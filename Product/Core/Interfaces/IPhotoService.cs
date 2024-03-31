using Product.Core.Models;

namespace Product.Core.Interfaces
{
    public interface IPhotoService
    {
        public Task<ResponseDto> CreateAsync(Guid productId, List<IFormFile> files);

        public Task<List<Image>?> GetAsync(Guid productId);

        public Task<ResponseDto> DeleteAsync(Guid productId, Guid id);
    }
}