using Microsoft.EntityFrameworkCore;
using Product.Context;
using Product.Core.Common.Exceptions;
using Product.Core.Common.Messages;
using Product.Core.Dtos.Category;
using Product.Core.Mapper;
using Product.Core.Models;

namespace Product.Core.Repositories.Impl
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Category> Save(CategoryRequest categoryRequest)
        {
            var existingCategory = this.ExistByName(categoryRequest.Name);

            if (existingCategory)
            {
                throw new BadRequestException("Category already exist!");
            }

            var result = CategoryMapper.MapFromDto(categoryRequest);

            await _context.Categories.AddAsync(result);
            await _context.SaveChangesAsync();

            return result;
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool ExistByName(string name)
        {
            return _context.Categories.Any(n => n.Name == name);
        }

        public async Task<Category> FindById(Guid id)
        {
            return await _context.Categories.FirstOrDefaultAsync(x => x.Id == id) ?? throw new NotFoundException(ErrorMessage.PRODUCT_NOT_FOUND);
        }

        public async Task<Category> Update(Guid id, CategoryRequest request)
        {
            var existingCategory = await this.FindById(id);

            existingCategory.Name = request.Name;
            existingCategory.UpdateAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return existingCategory;
        }

        public async Task<Category> GetDetail(Guid id)
        {
            return await this.FindById(id);
        }

        public async Task<List<Category>> FindAll()
        {
            var listCategories = await _context.Categories.ToListAsync();
            return listCategories;
        }

        public async Task<bool> Delete (Guid id)
        {
            var existingCategory = await this.FindById(id);
            _context.Categories.Remove(existingCategory);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}