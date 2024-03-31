using Product.Core.Dtos.Review;
using Product.Core.Models;

namespace Product.Core.Mapper
{
    public class ReviewMapper
    {
        public static Reviews MapFromDto(ReviewDto reviewDto, Guid productId)
        {
            return new Reviews
            {
                Content = reviewDto.Content,
                Start = reviewDto.Start,
                CustomerName = reviewDto.CustomerName,
                CustomerAvatar = reviewDto.CustomerAvatar,
                CustomerId = reviewDto.CustomerId,
                BookId = productId
            };
        }
    }
}