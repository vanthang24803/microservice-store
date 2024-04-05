using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Product.Core.Dtos.Blogs
{
    public class BlogDto
    {
        [MaxLength(255, ErrorMessage = "Title <= 255 characters")]
        [Required(ErrorMessage = "Title Required")]
        public string Title { get; set; } = string.Empty;

        [NotNull]
        [Required(ErrorMessage = "Thumbnail Required")]

        public string Thumbnail { get; set; } = string.Empty;

        [NotNull]
        [Required(ErrorMessage = "Content Required")]

        public string Content { get; set; } = string.Empty;

        [MaxLength(255)]
        [Required(ErrorMessage = "AuthorName Required")]

        public string AuthorName { get; set; } = string.Empty;

        [NotNull]
        [Required(ErrorMessage = "AuthorId Required")]
        public string AuthorId { get; set; } = string.Empty;

        [NotNull]
        [Required(ErrorMessage = "AuthorAvatar Required")]
        public string AuthorAvatar { get; set; } = string.Empty;
    }
}