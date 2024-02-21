using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Core.Dtos
{
    public class ForgotPasswordDto
    {
        public string Email { get; set; } = string.Empty;
    }
}