using Product.Core.Dtos.Response;
using Product.Core.Dtos.Review;
using Product.Core.Models;

namespace Product.Core.Interfaces
{
    public interface IReviewService
    {
        Task<IResponse> CreateAsync(Guid productId, ReviewDto reviewDto, List<IFormFile>? files);

        Task<IResponse> UpdateAsync(Guid productId, Guid reviewId, UpdateReview updateReview);

        Task<List<Reviews>> FindAllAsync(Guid productId);

        Task<IResponse> DeleteAsync(Guid productId, Guid reviewId);

        Task<IResponse> IsExist(Guid productId, Guid reviewId);
    }
}