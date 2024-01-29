using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Product.Core.Dtos.Category;
using Product.Core.Dtos.Option;

namespace Product.Core.Dtos.Book
{
    public class CreateBookDto
    {
        public string Name { get; set; }

        public string Brand { get; set; }

        public string Thumbnail { get; set; }

        public string Category { get; set; }

        public List<OptionDto> Options { get; set; } = [];

        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
    }
}