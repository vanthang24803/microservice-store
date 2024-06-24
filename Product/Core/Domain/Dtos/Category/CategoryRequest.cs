using System.ComponentModel.DataAnnotations;


namespace Product.Core.Dtos.Category
{
    public class CategoryRequest
    {
        [Required(ErrorMessage = "Category is required")]
        public string Name { get; set; } = string.Empty;
    }
}