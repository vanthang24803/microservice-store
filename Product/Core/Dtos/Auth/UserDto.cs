using System.ComponentModel.DataAnnotations;

namespace Product.Core.Dtos.Auth
{
    public class UserDto
    {
        [Required]
        public string Id { get; set; } = string.Empty;
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string Avatar { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        public int TotalOrder { get; set; }
        [Required]
        public int TotalPrice { get; set; }
    }
}