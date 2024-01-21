using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Core.Dtos.Category
{
    public class AddProductCategoryDto
    {
        [Required(ErrorMessage = "Category id is request")]
        public Guid CategoryId { get; set; }
    }
}