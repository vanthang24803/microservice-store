using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Core.Dtos.Billboard
{
    public class UpdateBillboard
    {
        [Required(ErrorMessage = "Url is required")]
        public string Url { get; set; }
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;

    }
}