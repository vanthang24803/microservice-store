using System.ComponentModel.DataAnnotations;


namespace Product.Core.Dtos.Billboard
{
    public class CreateBillboard
    {
        [Required(ErrorMessage = "Url is required")]
        public string Url { get; set; } = string.Empty;

        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
    }
}