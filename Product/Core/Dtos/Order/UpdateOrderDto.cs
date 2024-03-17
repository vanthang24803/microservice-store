using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Product.Core.Enum;

namespace Product.Core.Dtos.Order
{
    public class UpdateOrderDto
    {
        [Required(ErrorMessage = "Status is required")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Status Status { get; set; }
    }
}