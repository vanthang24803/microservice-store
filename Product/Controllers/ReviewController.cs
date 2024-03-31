using Microsoft.AspNetCore.Mvc;
using Product.Core.Dtos.Review;
using Product.Core.Interfaces;
using Product.Core.Utils;

namespace Product.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        [Route("{productId}/review")]

        public async Task<IActionResult> CreateReview([FromRoute] Guid productId, [FromForm] ReviewDto review, List<IFormFile>? files)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _reviewService.CreateAsync(productId, review, files);

            if (result.IsSucceed)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete]
        [Route("{productId}/review/{reviewId}")]

        public async Task<IActionResult> DeleteReview([FromRoute] Guid productId, [FromRoute] Guid reviewId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _reviewService.DeleteAsync(productId, reviewId);
            if (result.IsSucceed)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut]
        [Route("{productId}/review/{reviewId}")]

        public async Task<IActionResult> UpdateReview([FromRoute] Guid productId, [FromRoute] Guid reviewId, [FromBody] UpdateReview update)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _reviewService.UpdateAsync(productId, reviewId, update);

            if (result.IsSucceed)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet]
        [Route("{productId}/review")]

        public async Task<IActionResult> FindReviewByProduct([FromRoute] Guid productId, [FromQuery] QueryReview query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _reviewService.FindAllAsync(productId, query);

            if (result is null)
            {
                return NotFound("Review not found");
            }

            return Ok(result);

        }

    }
}