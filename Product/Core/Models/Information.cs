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

        public string Author { get; set; } = string.Empty;

        public string Translator { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public string Format { get; set; } = string.Empty;

        public string NumberOfPage { get; set; } = string.Empty;

        public string ISBN { get; set; } = string.Empty;

        public string Publisher { get; set; } = string.Empty;

        public string Company { get; set; } = string.Empty;

        public string Gift { get; set; } = string.Empty;

        public string Price { get; set; } = string.Empty;

        public string Released { get; set; } = string.Empty;

        public string Introduce { get; set; } = string.Empty;

        public Guid? BookId { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
    }
}