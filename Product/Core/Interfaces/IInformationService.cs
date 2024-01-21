using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Product.Core.Dtos.Information;
using Product.Core.Models;

namespace Product.Core.Interfaces
{
    public interface IInformationService
    {
        Task<Information?> GetAsync(Guid productId);

        Task<ResponseDto> CreateAsync(Guid productId,
        CreateInformation createInformation);

        Task<ResponseDto> UpdateAsync(Guid productId, Guid id, UpdateInformation updateInformation);

        Task<ResponseDto> DeleteAsync(Guid productId , Guid id);

    }
}