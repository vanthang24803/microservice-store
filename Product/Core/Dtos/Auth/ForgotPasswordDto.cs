using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Core.Dtos.Auth
{
    public class ForgotPasswordDto
    {
         public string Email { get; set; } = string.Empty;
    }
}