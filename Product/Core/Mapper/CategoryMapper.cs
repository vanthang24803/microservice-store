using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Product.Core.Dtos.Category;
using Product.Core.Models;

namespace Product.Core.Mapper
{
    public class CategoryMapper
    {
        public static Category MapFromDto(CreateCategoryDto categoryDto)
        {
            return new Category
            {
                Name = categoryDto.Name,
                CreateAt = categoryDto.CreateAt,
                UpdateAt = categoryDto.UpdateAt
            };
        }

        public static Category MapFromUpdateDto(CreateCategoryDto categoryDto)
        {
            return new Category
            {
                Name = categoryDto.Name,
                UpdateAt = categoryDto.UpdateAt
            };
        }
    }
}