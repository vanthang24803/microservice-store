using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Core.Dtos.Option
{
    public class OptionDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Sale { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public bool Status { get; set; } = true;

        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
    }
}