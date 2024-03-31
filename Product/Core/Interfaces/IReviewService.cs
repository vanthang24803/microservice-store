using Product.Core.Dtos.Response;
using Product.Core.Dtos.Review;
using Product.Core.Models;
using Product.Core.Utils;

namespace Product.Core.Interfaces
{
    public interface IReviewService
    {
        Task<IResponse> CreateAsync(Guid productId, ReviewDto reviewDto, List<IFormFile>? files);

        Task<IResponse> UpdateAsync(Guid productId, Guid reviewId, UpdateReview updateReview);

        Task<List<Reviews>> FindAllAsync(Guid productId , QueryReview query);

        Task<IResponse> DeleteAsync(Guid productId, Guid reviewId);

        Task<IResponse> IsExist(Guid productId, Guid reviewId);
    }
}