using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Product.Context;
using Product.Core.Dtos.Option;
using Product.Core.Interfaces;
using Product.Core.Mapper;
using Product.Core.Models;

namespace Product.Core.Services
{
    public class OptionsService : IOptionsService
    {
        private readonly ApplicationDbContext _context;

        public OptionsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseDto> CreateAsync(Guid productId, CreateOptionsDto createOptionsDto)
        {
            var existingProduct = await _context.Books.FindAsync(productId);

            if (existingProduct is null)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Product not found"
                };
            }

            var result = OptionMapper.MapFromDto(createOptionsDto, productId);

            await _context.Options.AddAsync(result);

            await _context.SaveChangesAsync();

            return new ResponseDto()
            {
                IsSucceed = true,
                Message = "Option created successfully"
            };
        }

        public async Task<ResponseDto> DeleteAsync(Guid productId, Guid id)
        {
            var existingProduct = await _context.Books.FindAsync(productId);

            if (existingProduct is null)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Product not found"
                };
            }

            var existingOption = await _context.Options.FindAsync(id);

            if (existingOption is null)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Option not found"
                };
            }

            _context.Options.Remove(existingOption);

            await _context.SaveChangesAsync();

            return new ResponseDto()
            {
                IsSucceed = true,
                Message = "Option deleted successfully"
            };

        }

        public async Task<List<Options>?> GetAsync(Guid productId)
        {
            var existingProduct = await _context.Books.FindAsync(productId);

            if (existingProduct is null)
            {
                return null;

            }

            var listOptions = await _context.Options.Where(i => i.BookId == productId).ToListAsync();

            return listOptions;
        }

        public async Task<ResponseDto> UpdateAsync(Guid productId, Guid id, UpdateOptionsDto updateOptionsDto)
        {
            var existingProduct = await _context.Books.FindAsync(productId);

            if (existingProduct is null)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Product not found"
                };
            }

            var existingOption = await _context.Options.FindAsync(id);

            if (existingOption is null)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Option not found"
                };
            }

            if (updateOptionsDto.Quantity > 0)
            {
                existingOption.Status = true;
            }
            else
            {
                existingOption.Status = false;
            }

            existingOption.Name = updateOptionsDto.Name;
            existingOption.Sale = updateOptionsDto.Sale;
            existingOption.Price = updateOptionsDto.Price;
            existingOption.Quantity = updateOptionsDto.Quantity;
            existingOption.UpdateAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new ResponseDto()
            {
                IsSucceed = true,
                Message = "Option updated successfully"
            };
        }



        public async Task<Options?> GetDetailAsync(Guid productId, Guid id)
        {
            var existingProduct = await _context.Books.FindAsync(productId);

            if (existingProduct is null)
            {
                return null;
            }

            var existingOption = await _context.Options.FindAsync(id);

            if (existingOption is null)
            {
                return null;
            }

            return existingOption;

        }
    }
}