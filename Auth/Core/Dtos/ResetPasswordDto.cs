using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Core.Dtos
{
    public class ResetPasswordDto
    {
        public string NewPassword { get; set; } = string.Empty;
    }
}