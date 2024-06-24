using System.ComponentModel.DataAnnotations;


namespace Product.Core.Dtos.Option
{
    public class CreateOptionsDto
    {

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Sale is required")]
        public int Sale { get; set; } = 0;

        public int Quantity { get; set; }

        public double Price { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
    }
}