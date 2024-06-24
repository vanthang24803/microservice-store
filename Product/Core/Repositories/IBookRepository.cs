using Product.Core.Models;

namespace Product.Core.Repositories
{
    public interface IBookRepository
    {
        Task<Book> FindById(Guid productId);
    }
}