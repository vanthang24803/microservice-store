using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Core.Dtos.Voucher
{
    public class CreateVoucher
    {
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Tile is Required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Quantity is Required")]
        public double Quantity { get; set; } = 0;

        [Required(ErrorMessage = "Day is Required")]
        public int Day { get; set; } = 0;

        public bool Type { get; set; } = false;

        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
    }
}