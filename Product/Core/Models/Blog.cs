using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Product.Core.Models
{
    public class Blog
    {
        [Key]

        public Guid Id { get; set; } = Guid.NewGuid();

        [MaxLength(255)]
        public string Title { get; set; } = string.Empty;

        [NotNull]
        public string Thumbnail { get; set; } = string.Empty;
        [NotNull]
        public string Content { get; set; } = string.Empty;

        [MaxLength(255)]
        public string AuthorName { get; set; } = string.Empty;
        [NotNull]
        public string AuthorId { get; set; } = string.Empty;

        [NotNull]
        public string AuthorAvatar { get; set; } = string.Empty;

        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;

    }
}