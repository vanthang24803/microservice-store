using Microsoft.EntityFrameworkCore;
using Product.Context;
using Product.Core.Common.Exceptions;
using Product.Core.Common.Messages;
using Product.Core.Interfaces;
using Product.Core.Models;

namespace Product.Core.Repositories.Impl
{
    public class PhotoRepository : IPhotoRepository
    {

        private readonly ApplicationDbContext _context;

        private readonly IBookRepository _bookRepository;

        private readonly IUploadService _uploadService;

        public PhotoRepository(ApplicationDbContext context, IBookRepository bookRepository, IUploadService uploadService)
        {
            _context = context;
            _bookRepository = bookRepository;
            _uploadService = uploadService;
        }

        public async Task<string> Delete(Guid productId, Guid id)
        {
            var existingProduct = await _bookRepository.ExistById(productId);


            if (!existingProduct)
            {
                throw new NotFoundException(message: ErrorMessage.PRODUCT_NOT_FOUND);
            }

            var existingPhoto = await this.FindImageById(id);

            _context.Images.Remove(existingPhoto);
            await _context.SaveChangesAsync();

            return "Image deleted successfully!";
        }

        public async Task<List<Image>> GetAll(Guid productId)
        {
            var existingProduct = await _bookRepository.ExistById(productId);

            if (!existingProduct)
            {
                throw new NotFoundException(message: ErrorMessage.PRODUCT_NOT_FOUND);
            }

            var listImages = await _context.Images
                              .Where(i => i.BookId == productId)
                              .ToListAsync();

            return listImages;

        }

        public async Task<List<Image>> Save(Guid productId, List<IFormFile> files)
        {
            var existingProduct = await _bookRepository.FindById(productId);

            var images = new List<Image>();

            if (files.Count == 0)
            {
                throw new BadRequestException("No files to upload!");
            }

            foreach (var file in files)
            {
                var result = await _uploadService.AddPhotoAsync(file);

                if (result.Error != null)
                {
                    throw new BadRequestException(result.Error.Message);
                }

                var image = new Image
                {
                    Url = result.SecureUrl.AbsoluteUri,
                    BookId = productId,
                };

                await _context.Images.AddAsync(image);
                existingProduct.Images.Add(image);
                images.Add(image);
            }

            await _context.SaveChangesAsync();

            return images;
        }

        private async Task<Image> FindImageById(Guid id)
        {
            return await _context.Images.FirstOrDefaultAsync(x => x.Id == id) ?? throw new NotFoundException("Photo not found!");
        }
    }
}