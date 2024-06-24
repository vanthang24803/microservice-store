using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Product.Core.Models
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = string.Empty;

        [JsonIgnore]
        public List<Book> Products { get; set; } = [];

        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;

    }
}