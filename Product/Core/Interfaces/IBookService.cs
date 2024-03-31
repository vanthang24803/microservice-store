using Product.Core.Dtos.Book;
using Product.Core.Models;
using Product.Core.Utils;

namespace Product.Core.Interfaces
{
    public interface IBookService
    {
        public Task<ResponseDto> CreateAsync(CreateBookDto createBookDto);

        public Task<ResponseDto> DeleteAsync(Guid id);

        public Task<ResponseDto> UpdateAsync(Guid id, UpdateBookDto updateBookDto);

        public Task<List<BookDto>> GetAsync(QueryObject query);

        public Task<Book?> GetDetailAsync(Guid id);

        public Task<List<Book>> GetBooksSelling();

        public Task<double> GetTotalProduct();

        public Task<ResponseDto> UpdateDetailAsync(Guid id , DetailDto detailDto); 
    }
}