using System.ComponentModel.DataAnnotations;

namespace Product.Core.Dtos.Review
{
    public class ReviewDto
    {
        [MaxLength(255, ErrorMessage = "Customer Name is too long")]
        public string Content { get; set; } = string.Empty;
        [Range(0, 5, ErrorMessage = "Value must be between 0 and 5")]
        public float Start { get; set; }

        [MaxLength(255, ErrorMessage = "Customer Name is too long")]
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerId { get; set; } = string.Empty;
        public string CustomerAvatar { get; set; } = string.Empty;

    }
}