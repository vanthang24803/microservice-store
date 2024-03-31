using Microsoft.EntityFrameworkCore;
using Product.Context;
using Product.Core.Dtos.Book;
using Product.Core.Interfaces;
using Product.Core.Mapper;
using Product.Core.Models;
using Product.Core.Utils;

namespace Product.Core.Services
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseDto> CreateAsync(CreateBookDto createBookDto)
        {
            var result = BookMapper.MapFromDto(createBookDto);

            var existingCategory = await _context.Categories.FindAsync(Guid.Parse(createBookDto.Category));

            if (existingCategory is null)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Category not found"
                };
            }

            result.Categories.Add(existingCategory);

            await _context.SaveChangesAsync();


            if (existingCategory is null)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Category not found"
                };
            }

            await _context.Books.AddAsync(result);

            await _context.SaveChangesAsync();

            return new ResponseDto()
            {
                IsSucceed = true,
                Message = "Product created successfully"
            };
        }

        public async Task<ResponseDto> DeleteAsync(Guid id)
        {
            var existingProduct = await _context.Books.FindAsync(id);

            if (existingProduct is null)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Product not found"
                };
            }

            _context.Options.RemoveRange(existingProduct.Options);
            _context.Images.RemoveRange(existingProduct.Images);
            _context.Remove(existingProduct);
            await _context.SaveChangesAsync();

            return new ResponseDto()
            {
                IsSucceed = true,
                Message = "Product deleted successfully"
            };
        }

        public async Task<List<BookDto>> GetAsync(QueryObject query)
        {
            var listProducts = await _context.Books
                .Include(i => i.Images.OrderByDescending(s => s.CreateAt))
                .Include(c => c.Categories)
                .Include(o => o.Options)
                .AsQueryable()
                .ToListAsync();


            var filter = new ProductFilter();
            listProducts = filter.ApplyFilters(listProducts, query);

            var response = listProducts.Select(product => BookMapper.MapToDto(product)).ToList();

            return response;

        }

        public async Task<Book?> GetDetailAsync(Guid id)
        {
            var existingProduct = await _context.Books
                    .Include(c => c.Categories)
                    .Include(i => i.Images)
                    .Include(o => o.Options)
                    .Include(r => r.Reviews).ThenInclude(rv => rv.Images)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (existingProduct is null)
            {
                return null;
            }

            return existingProduct;
        }


        public async Task<ResponseDto> UpdateAsync(Guid id, UpdateBookDto updateBookDto)
        {
            var existingProduct = await _context.Books.FindAsync(id);

            if (existingProduct is null)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Product not found"
                };
            }

            existingProduct.Name = updateBookDto.Name;
            existingProduct.Brand = updateBookDto.Brand;
            existingProduct.Thumbnail = updateBookDto.Thumbnail;
            existingProduct.UpdateAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new ResponseDto()
            {
                IsSucceed = true,
                Message = "Book updated successfully",
            };

        }

        public async Task<double> GetTotalProduct()
        {
            var totalQuantity = await _context.Books
                            .SelectMany(b => b.Options)
                            .SumAsync(o => o.Quantity);

            return totalQuantity;
        }

        public async Task<List<Book>> GetBooksSelling()
        {
            List<Book> topSellingBooks = [];
            var listTopSellingProducts = await _context.OrderDetails
                                .GroupBy(od => od.ProductId)
                                .Select(g =>
                                    new { ProductId = g.Key, TotalQuantity = g.Sum(x => x.Quantity) })
                                .OrderByDescending(x => x.TotalQuantity)
                                .Take(10)
                                .ToListAsync();

            foreach (var product in listTopSellingProducts)
            {
                var book = await _context.Books
                    .Include(b => b.Images.OrderByDescending(s => s.CreateAt))
                    .Include(b => b.Categories)
                    .Include(b => b.Options)
                    .FirstOrDefaultAsync(i => i.Id == Guid.Parse(product.ProductId))

                ;

                if (book != null)
                {
                    topSellingBooks.Add(book);
                }
            }

            return topSellingBooks;
        }

        public async Task<ResponseDto> UpdateDetailAsync(Guid id, DetailDto detail)
        {
            var exitingBook = await _context.Books.FindAsync(id);

            if (exitingBook is null)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Product not found"
                };
            }

            exitingBook.Detail = detail.Detail;
            exitingBook.Introduction = detail.Introduction;

            await _context.SaveChangesAsync();

            return new ResponseDto()
            {
                IsSucceed = true,
                Message = "Update Detail Success"
            };

        }
    }
}