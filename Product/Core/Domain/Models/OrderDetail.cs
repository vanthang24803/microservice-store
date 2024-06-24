using System.ComponentModel.DataAnnotations;


namespace Product.Core.Models
{
    public class OrderDetail
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string ProductId { get; set; } = string.Empty;
        public string OptionId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Thumbnail { get; set; } = string.Empty;
        public string Option { get; set; } = string.Empty;
        public int Price { get; set; }
        public int Sale { get; set; }
        public int Quantity { get; set; }
        public string? OrderId { get; set; }
    }
}