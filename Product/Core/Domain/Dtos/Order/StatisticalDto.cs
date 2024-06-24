namespace Product.Core.Dtos.Order
{
    public class StatisticalDto
    {
         public double TotalPrice { get; set; }

        public int TotalOrder { get; set; }

        public int TotalProduct { get; set; }

        public List<OrderDto> Orders { get; set; } = [];
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    }
}