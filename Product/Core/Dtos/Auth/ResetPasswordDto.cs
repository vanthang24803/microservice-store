using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Core.Dtos.Auth
{
    public class ResetPasswordDto
    {
         public string NewPassword { get; set; } = string.Empty;
    }
}