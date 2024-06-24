using Product.Core.Dtos.Category;
using Product.Core.Models;

namespace Product.Core.Mapper
{
    public class CategoryMapper
    {
        public static Category MapFromDto(CategoryRequest request)
        {
            return new Category
            {
                Name = request.Name,
            };
        }

        public static Category MapFromUpdateDto(CategoryRequest request)
        {
            return new Category
            {
                Name = request.Name,
            };
        }
    }
}