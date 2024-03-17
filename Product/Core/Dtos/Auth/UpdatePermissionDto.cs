using System.ComponentModel.DataAnnotations;

namespace Product.Core.Dtos.Auth
{
    public class UpdatePermissionDto
    {
        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; } = string.Empty;
    }
}