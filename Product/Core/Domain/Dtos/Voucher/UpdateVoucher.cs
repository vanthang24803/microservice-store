using System.ComponentModel.DataAnnotations;


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
        public DateTime ShelfLife { get; set; }
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
    }
}