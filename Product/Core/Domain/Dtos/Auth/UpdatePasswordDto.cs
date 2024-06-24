
using System.ComponentModel.DataAnnotations;


namespace Product.Core.Dtos.Auth
{
    public class UpdatePasswordDto
    {
        [Required(ErrorMessage = "Old password is required")]

        public string OldPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "New password is required")]

        public string NewPassword { get; set; } = string.Empty;
    }
}