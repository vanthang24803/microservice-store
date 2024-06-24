

using System.ComponentModel.DataAnnotations;

namespace Product.Core.Dtos.Auth
{
    public class UpdateProfileDto
    {
        [Required(ErrorMessage = "FirsName is Required")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "LastName is Required")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; } = string.Empty;

    }
}