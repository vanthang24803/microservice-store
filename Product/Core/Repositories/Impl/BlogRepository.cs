using Microsoft.EntityFrameworkCore;
using Product.Context;
using Product.Core.Common.Exceptions;
using Product.Core.Dtos.Blogs;
using Product.Core.Mapper;
using Product.Core.Models;

namespace Product.Core.Repositories.Impl
{
    public class BlogRepository : IBlogRepository
    {
        private readonly ApplicationDbContext _context;
        public BlogRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<string> Delete(Guid id)
        {
            var existingBlog = await this.FindBlogById(id);

            _context.Blogs.Remove(existingBlog);
            await _context.SaveChangesAsync();

            return "OK";

        }

        public async Task<Blog> FindBlogById(Guid id)
        {
            return await _context.Blogs.FirstOrDefaultAsync(x => x.Id == id) ?? throw new NotFoundException("Blog not found!");
        }

        public async Task<List<Blog>> GetAllBlogs()
        {
            return await _context.Blogs.ToListAsync();
        }

        public async Task<List<Blog>> GetBlogsByAuthorId(Guid authorId)
        {
            return await _context.Blogs
                  .Where(b => b.AuthorId == authorId.ToString())
                  .OrderByDescending(n => n.CreateAt)
                  .ToListAsync();
        }

        public async Task<Blog> Save(BlogDto blog)
        {
            var newBlog = BlogMapper.MapFromDto(blog);

            _context.Blogs.Add(newBlog);
            await _context.SaveChangesAsync();

            return newBlog;
        }

        public async Task<Blog> Update(Guid id, UpdateBlogDto blog)
        {
            var existingBlog = await this.FindBlogById(id);

            existingBlog.Title = blog.Title;
            existingBlog.Content = blog.Content;
            existingBlog.Thumbnail = blog.Thumbnail;
            existingBlog.UpdateAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return existingBlog;
        }
    }
}