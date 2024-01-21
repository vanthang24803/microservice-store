using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Product.Core.Dtos.Category;
using Product.Core.Models;

namespace Product.Core.Interfaces
{
    public interface ICategoryProductService
    {
        public Task<ResponseDto> AddCategoryToProduct(Guid idProduct, Guid idCategory);

        public Task<ResponseDto> DeleteCategoryToProduct(Guid idProduct, Guid idCategory);

    }
}