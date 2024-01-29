using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Product.Context;
using Product.Core.Dtos.Book;
using Product.Core.Dtos.Category;
using Product.Core.Dtos.Image;
using Product.Core.Dtos.Option;
using Product.Core.Interfaces;
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
            var result = MapFromDto(createBookDto);

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

            var response = listProducts.Select(product => MapToDto(product)).ToList();

            return response;

        }

        public async Task<Book?> GetDetailAsync(Guid id)
        {
            var existingProduct = await _context.Books
                    .Include(a => a.Information)
                    .Include(c => c.Categories)
                    .Include(i => i.Images)
                    .Include(o => o.Options)
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


        public Book MapFromDto(CreateBookDto createBookDto)
        {
            return new Book
            {
                Name = createBookDto.Name,
                Brand = createBookDto.Brand,
                Options = createBookDto.Options.Select(o => new Options
                {
                    Id = o.Id,
                    Name = o.Name,
                    Price = o.Price,
                    Sale = o.Sale,
                    Status = o.Status,
                    Quantity = o.Quantity,
                    CreateAt = o.CreateAt,
                    UpdateAt = o.UpdateAt,
                }).ToList(),
                Thumbnail = createBookDto.Thumbnail,
                CreateAt = createBookDto.CreateAt,
                UpdateAt = createBookDto.UpdateAt
            };
        }

        public BookDto MapToDto(Book product)
        {
            return new BookDto
            {
                Id = product.Id,
                Name = product.Name,
                Brand = product.Brand,
                Thumbnail = product.Thumbnail,
                Categories = product.Categories.Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    CreateAt = c.CreateAt,
                    UpdateAt = c.UpdateAt,
                }).ToList(),
                Images = product.Images.Select(i => new ImageDto
                {
                    Id = i.Id,
                    Url = i.Url,
                    CreateAt = i.CreateAt,
                    UpdateAt = i.UpdateAt,
                }).ToList(),
                Options = product.Options.Select(o => new OptionDto
                {
                    Id = o.Id,
                    Name = o.Name,
                    Price = o.Price,
                    Sale = o.Sale,
                    Status = o.Status,
                    Quantity = o.Quantity,
                    CreateAt = o.CreateAt,
                    UpdateAt = o.UpdateAt,
                }).ToList(),
                CreateAt = product.CreateAt,
                UpdateAt = product.UpdateAt,
            };
        }
    }
}