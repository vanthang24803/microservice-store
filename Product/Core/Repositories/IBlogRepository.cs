using Product.Core.Dtos.Blogs;
using Product.Core.Models;

namespace Product.Core.Repositories
{
    public interface IBlogRepository
    {
        Task<Blog> Save(BlogDto blog);

        Task<Blog> Update(Guid id, UpdateBlogDto blog);

        Task<Blog> FindBlogById(Guid id);

        Task<List<Blog>> GetAllBlogs();

        Task<List<Blog>> GetBlogsByAuthorId (Guid authorId);
        Task<string> Delete(Guid id);
    }
}