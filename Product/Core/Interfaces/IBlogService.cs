using Product.Core.Common.Utils;
using Product.Core.Dtos.Blogs;
using Product.Core.Dtos.Response;
using Product.Core.Models;

namespace Product.Core.Interfaces
{
    public interface IBlogService
    {
       Task<Response<Blog>> CreateAsync(BlogDto blogDto);

       Task<Response<Blog>> UpdateAsync(Guid id, UpdateBlogDto update);

        Task<Blog> GetDetailAsync(Guid id);

        Task<List<Blog>> GetBlogsAsync();

        Task<List<Blog>> GetBlogByAuthorAsync(Guid authorId);

        Task<string> DeleteAsync(Guid id);
    }
}