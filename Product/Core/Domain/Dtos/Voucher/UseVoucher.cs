
using System.ComponentModel.DataAnnotations;

namespace Product.Core.Dtos.Voucher
{
    public class UseVoucher
    {
        [Required(ErrorMessage = "Code is required")]

        public string Code { get; set; } = string.Empty;
    }
}