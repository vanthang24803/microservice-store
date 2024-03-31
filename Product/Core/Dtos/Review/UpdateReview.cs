using System.ComponentModel.DataAnnotations;

namespace Product.Core.Dtos.Review
{
    public class UpdateReview
    {
        [MaxLength(255, ErrorMessage = "Customer Name is too long")]
        public string Content { get; set; } = string.Empty;
        [Range(0, 5, ErrorMessage = "Value must be between 0 and 5")]
        public float Start { get; set; }

    }
}