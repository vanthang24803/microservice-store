using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Product.Core.Enum;

namespace Product.Core.Models
{
    public class Order
    {
        [Key]
        public string Id { get; set; } = string.Empty;
        public List<OrderDetail> Products { get; set; } = [];
        public string Address { get; set; } = string.Empty;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Payment Payment { get; set; }
        public bool Shipping { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Status Status { get; set; } = Status.PENDING;
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string NumberPhone { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    }
}