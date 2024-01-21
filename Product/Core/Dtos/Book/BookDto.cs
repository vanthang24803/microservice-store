using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Product.Core.Dtos.Category;
using Product.Core.Dtos.Image;
using Product.Core.Dtos.Option;

namespace Product.Core.Dtos.Book
{
    public class BookDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Thumbnail { get; set; }
        public List<CategoryDto> Categories { get; set; } = [];
        public List<OptionDto> Options { get; set; } = [];
        public List<ImageDto> Images { get; set; } = [];
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
    }
}