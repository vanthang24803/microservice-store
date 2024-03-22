

using System.ComponentModel.DataAnnotations;

namespace Product.Core.Dtos.Voucher
{
    public class VoucherRequest
    {
        [Required(ErrorMessage = "Code is Required")]
        public string Code { get; set; } = string.Empty;
    }
}