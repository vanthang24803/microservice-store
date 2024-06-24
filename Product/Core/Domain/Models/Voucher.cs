using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Core.Models
{
    public class Voucher
    {
        [Key]

        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;

        public double Quantity { get; set; }

        public bool Type { get; set; } = false;

        public int Day { get; set; }

        public bool Expire { get; set; } = false;

        public double Discount { get; set; }

        public DateTime ShelfLife { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
    }
}