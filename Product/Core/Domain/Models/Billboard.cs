using System.ComponentModel.DataAnnotations;

namespace Product.Core.Models
{
    public class Billboard
    {
        [Key]

        public Guid Id { get; set; } = Guid.NewGuid();

        public string Thumbnail { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;

        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;

    }
}