using System.ComponentModel.DataAnnotations;

namespace Product.Core.Dtos.Book
{
    public class UpdateBookDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Brand { get; set; } = string.Empty;

        [Required]
        public string Thumbnail { get; set; } = string.Empty;

        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
    }
}