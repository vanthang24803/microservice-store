using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Core.Dtos.Book
{
    public class UpdateBookDto
    {
        public string Name { get; set; }

        public string Brand { get; set; }

        public string Thumbnail { get; set; }

        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
    }
}