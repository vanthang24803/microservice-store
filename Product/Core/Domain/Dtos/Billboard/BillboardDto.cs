
using System.ComponentModel.DataAnnotations;

namespace Product.Core.Domain.Dtos.Billboard
{
    public class BillboardDto
    {
        [Required(ErrorMessage = "Url is required")]
        public string Url { get; set; } = string.Empty;
    }
}