using System.ComponentModel.DataAnnotations;

namespace Product.Core.Dtos.Voucher
{
    public class ExtendVoucher
    {
        [Required(ErrorMessage = "Day is Required")]
        public int Day { get; set; }
    }
}