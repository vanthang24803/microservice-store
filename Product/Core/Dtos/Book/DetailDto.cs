using System.ComponentModel.DataAnnotations;

namespace Product.Core.Dtos.Book
{
    public class DetailDto
    {   
        [Required(ErrorMessage = "Detail is Required")]
        public string Detail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Introduction is Required")]
        public string Introduction { get; set; } = string.Empty;
    }
}