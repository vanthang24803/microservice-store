using CloudinaryDotNet.Actions;

namespace Product.Core.Interfaces
{
    public interface IUploadService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);

        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}