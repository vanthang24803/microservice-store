using Microsoft.EntityFrameworkCore;
using Product.Context;
using Product.Core.Common.Exceptions;
using Product.Core.Common.Messages;
using Product.Core.Models;

namespace Product.Core.Repositories.Impl
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Book> FindById(Guid productId)
        {
            return await _context.Books.FirstOrDefaultAsync(x => x.Id == productId) ?? throw new NotFoundException(ErrorMessage.PRODUCT_NOT_FOUND);

        }
    }
}