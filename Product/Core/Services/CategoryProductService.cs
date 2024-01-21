using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Product.Context;
using Product.Core.Dtos.Category;
using Product.Core.Interfaces;
using Product.Core.Models;

namespace Product.Core.Services
{
    public class CategoryProductService : ICategoryProductService
    {
        private readonly ApplicationDbContext _context;

        public CategoryProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseDto> AddCategoryToProduct(Guid idProduct, Guid idCategory)
        {
            var existingProduct = await _context.Books.FindAsync(idProduct);
            if (existingProduct is null)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Product not found"
                };
            }

            var existingCategory = await _context.Categories.FindAsync(idCategory);

            if (existingCategory is null)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Category not found"
                };
            }

            existingProduct.Categories.Add(existingCategory);

            await _context.SaveChangesAsync();

            return new ResponseDto()
            {
                IsSucceed = true,
                Message = "Category added to product successfully",
            };
        }

        public async Task<ResponseDto> DeleteCategoryToProduct(Guid idProduct, Guid idCategory)
        {
            var existingProduct = await _context.Books.FindAsync(idProduct);
            if (existingProduct is null)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Product not found"
                };
            }

            var existingCategory = await _context.Categories.FindAsync(idCategory);

            if (existingCategory is null)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Category not found"
                };
            }

            existingProduct.Categories.Remove(existingCategory);

            await _context.SaveChangesAsync();

            return new ResponseDto()
            {
                IsSucceed = true,
                Message = "Category deleted to product successfully",
            };

        }
    }
}