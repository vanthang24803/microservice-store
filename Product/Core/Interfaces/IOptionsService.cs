using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Product.Core.Dtos.Option;
using Product.Core.Models;

namespace Product.Core.Interfaces
{
    public interface IOptionsService
    {
        public Task<ResponseDto> CreateAsync(Guid productId, CreateOptionsDto createOptionsDto);

        public Task<ResponseDto> UpdateAsync(Guid productId, Guid id, UpdateOptionsDto updateOptionsDto);

        public Task<List<Options>?> GetAsync(Guid productId);

        public Task<Options?> GetDetailAsync(Guid productId, Guid id);

        public Task<ResponseDto> DeleteAsync(Guid productId, Guid id);
    }
}