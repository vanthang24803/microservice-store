using Microsoft.EntityFrameworkCore;
using Product.Context;
using Product.Core.Dtos.Category;
using Product.Core.Interfaces;
using Product.Core.Mapper;
using Product.Core.Models;

namespace Product.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;
        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseDto> CreateAsync(CreateCategoryDto categoryDto)
        {
            var existingCategory = await _context.Categories
                .FirstOrDefaultAsync(c => c.Name == categoryDto.Name);

            if (existingCategory != null)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Category already exist"
                };
            }
            var result = CategoryMapper.MapFromDto(categoryDto);

            await _context.Categories.AddAsync(result);
            await _context.SaveChangesAsync();

            return new ResponseDto()
            {
                IsSucceed = true,
                Message = "Category created successfully"
            };
        }

        public async Task<ResponseDto> DeleteAsync(Guid id)
        {
            var existingCategory = await _context.Categories.FindAsync(id);
            if (existingCategory is null)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Category not found",
                };
            }

            _context.Categories.Remove(existingCategory);
            await _context.SaveChangesAsync();

            return new ResponseDto()
            {
                IsSucceed = true,
                Message = "Category deleted successfully",
            };
        }

        public async Task<List<Category>> GetAsync()
        {
            var listCategories = await _context.Categories.ToListAsync();
            return listCategories;
        }

        public async Task<ResponseDto> UpdateAsync(Guid id, UpdateCategoryDto updateCategoryDto)
        {
            var existingCategory = await _context.Categories.FindAsync(id);
            if (existingCategory is null)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Category not found",
                };
            }

            existingCategory.Name = updateCategoryDto.Name;
            existingCategory.UpdateAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new ResponseDto()
            {
                IsSucceed = true,
                Message = "Category updated successfully",
            };

        }

        public async Task<Category?> GetDetailAsync(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category is null)
            {
                return null;
            }

            return category;
        }
    }
}