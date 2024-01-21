using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Product.Core.Dtos.Category;
using Product.Core.Models;

namespace Product.Core.Interfaces
{
    public interface ICategoryService
    {
        public Task<ResponseDto> CreateAsync(CreateCategoryDto createCategoryDto);

        public Task<ResponseDto> UpdateAsync(Guid id, UpdateCategoryDto updateCategoryDto);

        public Task<List<Category>> GetAsync();

        public Task<ResponseDto> DeleteAsync(Guid id);
    }
}