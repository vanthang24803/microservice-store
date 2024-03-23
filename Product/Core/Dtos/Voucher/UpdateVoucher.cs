using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Core.Dtos.Voucher
{
    public class UpdateVoucher
    {
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tile is Required")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Quantity is Required")]
        public double Quantity { get; set; } = 0;

        [Required(ErrorMessage = "Discount is Required")]
        public double Discount { get; set; } = 0;
        [Required(ErrorMessage = "Day is Required")]
        public int Day { get; set; }

        public bool Type { get; set; } = false;

        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
    }
}