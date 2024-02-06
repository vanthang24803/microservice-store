using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using DocumentFormat.OpenXml.Office.CustomUI;
using Microsoft.EntityFrameworkCore;
using Product.Context;
using Product.Core.Interfaces;
using Product.Core.Models;

namespace Product.Core.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly ApplicationDbContext _context;

        private readonly IUploadService _upload;

        public PhotoService(ApplicationDbContext context, IUploadService upload)
        {
            _context = context;
            _upload = upload;
        }

        public async Task<ResponseDto> CreateAsync(Guid productId, List<IFormFile> files)
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

            if (files.Count == 0)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "No files to upload"
                };
            }


            foreach (var file in files)
            {

                var result = await _upload.AddPhotoAsync(file);

                if (result.Error != null)
                {
                    return new ResponseDto()
                    {
                        IsSucceed = false,
                        Message = result.Error.Message
                    };
                }

                var image = new Image
                {
                    Url = result.SecureUrl.AbsoluteUri,
                    BookId = productId,
                };

                await _context.Images.AddAsync(image);

                existingProduct.Images.Add(image);
            }

            await _context.SaveChangesAsync();

            return new ResponseDto()
            {
                IsSucceed = true,
                Message = "Image upload successfully!"
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

            var existingImage = await _context.Images.FindAsync(id);

            if (existingImage is null)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Product not found"
                };
            }

            _context.Images.Remove(existingImage);

            await _context.SaveChangesAsync();

            return new ResponseDto()
            {
                IsSucceed = true,
                Message = "Image deleted successfully!"
            };
        }

        public async Task<List<Image>?> GetAsync(Guid productId)
        {
            var existingProduct = await _context.Books.FindAsync(productId);

            if (existingProduct is null)
            {
                return null;
            }

            var listImages = await _context.Images.Where(i => i.BookId == productId).ToListAsync();

            return listImages;
        }
    }
}