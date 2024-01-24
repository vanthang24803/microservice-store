using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Order.core.Enum;

namespace Order.core.Models
{
    public class Order
    {
        [Key]
        public string Id { get; set; }
        public List<Product> Products { get; set; } = [];
        public string Address { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Payment Payment { get; set; }
        public bool Shipping { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Status Status { get; set; } = Status.PENDING;
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string NumberPhone { get; set; }
        public string Name { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    }
}