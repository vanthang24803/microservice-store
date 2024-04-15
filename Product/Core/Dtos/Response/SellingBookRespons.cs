using System.ComponentModel.DataAnnotations;
using Product.Core.Dtos.Response;

namespace Product.Core.Dtos.Auth
{
    public class SellingBookResponse : IResponse
    {
        [Required]
        public bool IsSucceed { get; set; }

        [Required]
        public required Products Products { get; set; }
    }

    public class Products
    {

        [Required]
        public List<Models.Book> Data { get; set; } = [];

        [Required]
        public int TotalSale { get; set; }
    }

}