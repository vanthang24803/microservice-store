using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Core.Dtos.Category
{
    public class UpdateCategoryDto
    {

        [Required(ErrorMessage = "Category is required")]
        public string Name { get; set; }

        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
    }
}