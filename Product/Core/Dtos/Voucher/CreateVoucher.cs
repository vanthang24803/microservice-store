using System.ComponentModel.DataAnnotations;

namespace Product.Core.Dtos.Voucher
{
    public class CreateVoucher
    {
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tile is Required")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Quantity is Required")]
        public double Quantity { get; set; } = 0;

        [Required(ErrorMessage = "Day is Required")]
        public int Day { get; set; } = 0;

        public bool Type { get; set; } = false;
        public int Discount { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
    }
}