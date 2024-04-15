using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Product.Core.Dtos.Category;
using Product.Core.Dtos.Image;
using Product.Core.Dtos.Option;

namespace Product.Core.Dtos.Book
{
    public class BookDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Brand { get; set; } = string.Empty;
        [Required]
        public string Thumbnail { get; set; } = string.Empty;
        [Required]
        public int Sold { get; set; } = 0;
        public List<CategoryDto> Categories { get; set; } = [];
        public List<OptionDto> Options { get; set; } = [];
        public List<ImageDto> Images { get; set; } = [];
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
    }
}