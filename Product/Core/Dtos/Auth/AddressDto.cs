using System.ComponentModel.DataAnnotations;

namespace Product.Core.Dtos.Auth
{
    public class AddressDto
    {
        [Required(ErrorMessage = "Address name is Required")]
        public string Name { get; set; } = string.Empty;
    }
}