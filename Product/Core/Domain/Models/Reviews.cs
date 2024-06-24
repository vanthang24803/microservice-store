using System.ComponentModel.DataAnnotations;

namespace Product.Core.Models
{
    public class Reviews
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [MaxLength(255, ErrorMessage = "Customer Name is too long")]
        public string Content { get; set; } = string.Empty;
        [Range(0, 5, ErrorMessage = "Value must be between 0 and 5")]
        public float Star { get; set; }

        [MaxLength(255, ErrorMessage = "Customer Name is too long")]
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerId { get; set; } = string.Empty;
        public string CustomerAvatar { get; set; } = string.Empty;
        public List<Image> Images { get; set; } = [];

        public Guid? BookId { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
    }
}