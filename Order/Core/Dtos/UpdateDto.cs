using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Order.core.Enum;

namespace Order.core.Dtos
{
    public class UpdateDto
    {
        [Required(ErrorMessage = "Status is required")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Status Status { get; set; }
    }
}