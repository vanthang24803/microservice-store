using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Product.Core.Dtos.Blogs
{
    public class UpdateBlogDto
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

    }
}