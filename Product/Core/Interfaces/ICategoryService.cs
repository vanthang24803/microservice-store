using Product.Core.Common.Utils;
using Product.Core.Dtos.Category;
using Product.Core.Models;

namespace Product.Core.Interfaces
{
    public interface ICategoryService
    {
        public Task<Response<Category>> CreateAsync(CategoryRequest create);

        public Task<Response<Category>> UpdateAsync(Guid id, CategoryRequest update);

        public Task<Category> GetDetailAsync(Guid id);

        public Task<List<Category>> GetAsync();

        public Task<string> DeleteAsync(Guid id);
    }
}