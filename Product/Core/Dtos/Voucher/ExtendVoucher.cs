using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Core.Dtos.Voucher
{
    public class ExtendVoucher
    {
        [Required(ErrorMessage = "Day is Required")]
        public int Day { get; set; }
    }
}