using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Product.Context;
using Product.Core.Dtos.Information;
using Product.Core.Interfaces;
using Product.Core.Models;

namespace Product.Core.Services
{
    public class InformationService : IInformationService
    {
        private readonly ApplicationDbContext _context;

        public InformationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseDto> CreateAsync(Guid productId, CreateInformation createInformation)
        {
            var exitingProduct = await _context.Books.FindAsync(productId);

            if (exitingProduct is null)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Product not found"
                };
            }

            var newInformation = new Information
            {
                Gift = createInformation.Gift,
                ISBN = createInformation.ISBN,
                Price = createInformation.Price,
                Format = createInformation.Format,
                Author = createInformation.Author,
                Company = createInformation.Company,
                Category = createInformation.Category,
                Released = createInformation.Released,
                Introduce = createInformation.Introduce,
                Publisher = createInformation.Publisher,
                Translator = createInformation.Translator,
                NumberOfPage = createInformation.NumberOfPage,
                BookId = productId,
            };

            _context.Information.Add(newInformation);

            await _context.SaveChangesAsync();

            return new ResponseDto()
            {
                IsSucceed = true,
                Message = "Information created successfully!"
            };
        }
        public async Task<ResponseDto> DeleteAsync(Guid productId, Guid id)
        {
            var exitingProduct = await _context.Books.FindAsync(productId);

            if (exitingProduct is null)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Product not found"
                };
            }

            var exitingInformation = await _context.Information.FindAsync(id);

            if (exitingInformation is null)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Information not found"
                };
            }

            _context.Information.Remove(exitingInformation);

            await _context.SaveChangesAsync();

            return new ResponseDto()
            {
                IsSucceed = true,
                Message = "Information deleted successfully"
            };
        }

        public async Task<Information?> GetAsync(Guid productId)
        {
            var exitingProduct = await _context.Books.FindAsync(productId);

            if (exitingProduct is null)
            {
                return null;
            }

            var information = await _context.Information.FirstOrDefaultAsync(c => c.BookId == productId);

            if (information is null)
            {
                return null;
            }

            return information;
        }

        public async Task<ResponseDto> UpdateAsync(Guid productId, Guid id, UpdateInformation updateInformation)
        {
            var exitingProduct = await _context.Books.FindAsync(productId);

            if (exitingProduct is null)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Product not found"
                };
            }

            var information = await _context.Information.FindAsync(id);

            if (information is null)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Information not found"
                };
            }

            information.Gift = updateInformation.Gift;
            information.ISBN = updateInformation.ISBN;
            information.Price = updateInformation.Price;
            information.Format = updateInformation.Format;
            information.Author = updateInformation.Author;
            information.Company = updateInformation.Company;
            information.Category = updateInformation.Category;
            information.Released = updateInformation.Released;
            information.Introduce = updateInformation.Introduce;
            information.Publisher = updateInformation.Publisher;
            information.Translator = updateInformation.Translator;
            information.NumberOfPage = updateInformation.NumberOfPage;

            await _context.SaveChangesAsync();

            return new ResponseDto()
            {
                IsSucceed = true,
                Message = "Information updated successfully"
            };
        }
    }
}