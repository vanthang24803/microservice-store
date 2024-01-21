using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Core.Dtos
{
    public class UpdatePasswordDto
    {
        [Required(ErrorMessage = "Password is required")]

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
    }
}