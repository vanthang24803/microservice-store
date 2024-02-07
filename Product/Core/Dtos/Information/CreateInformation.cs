using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Product.Core.Dtos.Information
{
    public class CreateInformation
    {

        [Required(ErrorMessage = "Author is required")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Translator is required")]
        public string Translator { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Format is required")]
        public string Format { get; set; }

        [Required(ErrorMessage = "Number of page is required")]
        public string NumberOfPage { get; set; }

        [Required(ErrorMessage = "ISBN is required")]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "Publisher is required")]
        public string Publisher { get; set; }

        [Required(ErrorMessage = "Company is required")]
        public string Company { get; set; }

        [Required(ErrorMessage = "Gift is required")]
        public string Gift { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public string Price { get; set; }

        [Required(ErrorMessage = "Released is required")]
        public string Released { get; set; }

        [Required(ErrorMessage = "Introduce is required")]
        public string Introduce { get; set; }

    }
}