using Product.Core.Dtos.Blogs;
using Product.Core.Dtos.Response;
using Product.Core.Models;

namespace Product.Core.Interfaces
{
    public interface IBlogService
    {
        Task<IResponse> CreateAsync(BlogDto blogDto);

        Task<IResponse> UpdateAsync(Guid id, UpdateBlogDto update);

        Task<Blog?> GetDetailAsync(Guid id);

        Task<List<Blog>> GetBlogsAsync();

        Task<IResponse> DeleteAsync(Guid id);
    }
}