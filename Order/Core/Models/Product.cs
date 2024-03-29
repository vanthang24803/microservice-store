using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Order.core.Models
{
    public class Product
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string ProductId { get; set; }
        public string OptionId { get; set; }
        public string Name { get; set; }
        public string Thumbnail { get; set; }
        public string Option { get; set; }
        public int Price { get; set; }
        public int Sale { get; set; }
        public int Quantity { get; set; }
        public string? OrderId { get; set; }
    }
}