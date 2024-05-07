namespace Product.Core.Interfaces
{
    public interface ICategoryProductService
    {
        public Task<ResponseDto> AddCategoryToProduct(Guid idProduct, Guid idCategory);

        public Task<ResponseDto> DeleteCategoryToProduct(Guid idProduct, Guid idCategory);

    }
}