using Product.Core.Dtos.Category;
using Product.Core.Models;

namespace Product.Core.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category> FindById(Guid id);

        bool ExistByName(string name);

        Task<Category> Save(CategoryRequest create);

        Task<Category> Update(Guid id, CategoryRequest update);

        Task<Category> GetDetail(Guid id);

        Task<List<Category>> FindAll();

        Task<bool> Delete(Guid id);
    }
}