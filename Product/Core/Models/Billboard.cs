using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Core.Models
{
    public class Billboard
    {
        [Key]

        public Guid Id { get; set; } = Guid.NewGuid();

        public string Thumbnail { get; set; }

        public string Url { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;

    }
}