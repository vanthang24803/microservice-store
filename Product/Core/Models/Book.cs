using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Core.Models
{
    public class Book
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = string.Empty;

        public string Brand { get; set; } = string.Empty;

        public string Thumbnail { get; set; } = string.Empty;

        public Information? Information { get; set; }

        public List<Category> Categories { get; set; } = new List<Category>();

        public List<Options> Options { get; set; } = new List<Options>();

        public List<Image> Images { get; set; } = new List<Image>();

        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
    }
}