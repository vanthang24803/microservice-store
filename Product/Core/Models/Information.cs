using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Core.Models
{
    public class Information
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Author { get; set; }

        public string Translator { get; set; }

        public string Category { get; set; }

        public string Format { get; set; }

        public string NumberOfPage { get; set; }

        public string ISBN { get; set; }

        public string Publisher { get; set; }

        public string Company { get; set; }

        public string Gift { get; set; }

        public string Price { get; set; }

        public string Released { get; set; }

        public string Introduce { get; set; }

        public Guid? BookId { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
    }
}