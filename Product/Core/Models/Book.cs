using System.ComponentModel.DataAnnotations;

namespace Product.Core.Models
{
    public class Book
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = string.Empty;

        public string Brand { get; set; } = string.Empty;

        public string Thumbnail { get; set; } = string.Empty;
        
        public List<Category> Categories { get; set; } = new List<Category>();

        public List<Options> Options { get; set; } = new List<Options>();

        public List<Image> Images { get; set; } = new List<Image>();

        public string Detail { get; set; } = string.Empty;

        public string Introduction { get; set; } = string.Empty;

        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
    }
}