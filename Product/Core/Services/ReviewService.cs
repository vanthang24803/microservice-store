using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.EntityFrameworkCore;
using Product.Context;
using Product.Core.Dtos.Response;
using Product.Core.Dtos.Review;
using Product.Core.Interfaces;
using Product.Core.Mapper;
using Product.Core.Models;

namespace Product.Core.Services
{
    public class ReviewService : IReviewService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUploadService _upload;

        public ReviewService(ApplicationDbContext context, IUploadService upload)
        {
            _context = context;
            _upload = upload;
        }

        public async Task<IResponse> CreateAsync(Guid productId, ReviewDto reviewDto, List<IFormFile>? files)
        {
            var existingProduct = await _context.Books.FindAsync(productId);

            if (existingProduct is null)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Product not found"
                };
            }

            var review = ReviewMapper.MapFromDto(reviewDto, productId);

            _context.Reviews.Add(review);

            if (files is not null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    var result = await _upload.AddPhotoAsync(file);

                    if (result.Error != null)
                    {
                        return new ResponseDto()
                        {
                            IsSucceed = false,
                            Message = result.Error.Message
                        };
                    }

                    var image = new Image
                    {
                        Url = result.SecureUrl.AbsoluteUri,
                        ReviewId = review.Id,
                    };

                    _context.Images.Add(image);
                    review.Images.Add(image);

                    await _context.SaveChangesAsync();

                    return new ResponseDto()
                    {
                        IsSucceed = true,
                        Message = "Review created successfully"
                    };
                }
            }

            await _context.SaveChangesAsync();

            return new ResponseDto()
            {
                IsSucceed = true,
                Message = "Review created successfully"
            };
        }

        public async Task<IResponse> DeleteAsync(Guid productId, Guid reviewId)
        {
            var existingProduct = await _context.Books.FindAsync(productId);

            if (existingProduct is null)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Product not found"
                };
            }

            var existingReview = await _context.Reviews.FindAsync(reviewId);

            if (existingReview is null)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Review not found"
                };
            }

            _context.Reviews.Remove(existingReview);

            await _context.SaveChangesAsync();

            return new ResponseDto()
            {
                IsSucceed = true,
                Message = "Review deleted successfully"
            };
        }

        public async Task<List<Reviews>> FindAllAsync(Guid productId)
        {

            return await _context.Reviews.Where(x => x.BookId == productId).ToListAsync();

        }

        public Task<IResponse> IsExist(Guid productId, Guid reviewId)
        {
            throw new NotImplementedException();
        }

        public async Task<IResponse> UpdateAsync(Guid productId, Guid reviewId, UpdateReview update)
        {
            var existingProduct = await _context.Books.FindAsync(productId);

            if (existingProduct is null)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Product not found"
                };
            }

            var existingReview = await _context.Reviews.FindAsync(reviewId);

            if (existingReview is null)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Review not found"
                };
            }

            existingReview.Start = update.Start;
            existingReview.Content = update.Content;

            await _context.SaveChangesAsync();

            return new ResponseDto()
            {
                IsSucceed = true,
                Message = "Review updated"
            };

        }
    }
}