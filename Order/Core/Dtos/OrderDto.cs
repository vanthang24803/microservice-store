using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Order.core.Enum;
using Order.core.Models;

namespace Order.Core.Dtos
{
    public class OrderDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public List<Product> Products { get; set; } = [];
        public string Address { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Payment Payment { get; set; }
        public bool Shipping { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Status Status { get; set; } = Status.PENDING;
        public string Voucher { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
        public string Email { get; set; }
        public string NumberPhone { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    }
}