using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Core.Dtos.Voucher
{
    public class UseVoucher
    {
        [Required(ErrorMessage = "Code is required")]

        public string Code { get; set; }
    }
}