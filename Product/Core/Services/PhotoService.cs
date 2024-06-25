using System.Net;
using Product.Core.Common.Utils;
using Product.Core.Interfaces;
using Product.Core.Models;
using Product.Core.Repositories;

namespace Product.Core.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly IPhotoRepository _photoRepository;

        public PhotoService(IPhotoRepository photoRepository)
        {

            _photoRepository = photoRepository;
        }

        public async Task<Response<List<Image>>> CreateAsync(Guid productId, List<IFormFile> files)
        {
            var photos = await _photoRepository.Save(productId, files);

            return new Response<List<Image>>(HttpStatusCode.Created, photos);
        }

        public async Task<string> DeleteAsync(Guid productId, Guid id)
        {
            return await _photoRepository.Delete(productId, id);
        }

        public async Task<List<Image>> GetAsync(Guid productId)
        {
            return await _photoRepository.GetAll(productId);
        }
    }
}