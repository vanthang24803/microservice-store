using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Core.Dtos.Option
{
    public class UpdateOptionsDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Sale is required")]
        public int Sale { get; set; } = 0;

        public int Quantity { get; set; }

        public double Price { get; set; }

        public bool Status { get; set; }

        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
    }
}