using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Product.Core.Models;

namespace Product.Core.Interfaces
{
    public interface IPhotoService
    {
        public Task<ResponseDto> CreateAsync(Guid productId, IFormFile file);

        public Task<List<Image>?> GetAsync(Guid productId);

        public Task<ResponseDto> DeleteAsync(Guid productId, Guid id);
    }
}