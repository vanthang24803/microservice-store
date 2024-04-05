using Microsoft.EntityFrameworkCore;
using Product.Context;
using Product.Core.Dtos.Blogs;
using Product.Core.Dtos.Response;
using Product.Core.Interfaces;
using Product.Core.Mapper;
using Product.Core.Models;

namespace Product.Core.Services
{
    public class BlogService : IBlogService
    {
        private readonly ApplicationDbContext _context;

        public BlogService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IResponse> CreateAsync(BlogDto blogDto)
        {
            var newBlog = BlogMapper.MapFromDto(blogDto);

            _context.Blogs.Add(newBlog);
            await _context.SaveChangesAsync();

            return new ResponseDto()
            {
                IsSucceed = true,
                Message = "Blog created successfully!"
            };
        }

        public async Task<IResponse> DeleteAsync(Guid id)
        {
            var existingBlog = await _context.Blogs.FindAsync(id);

            if (existingBlog == null)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Blog not found"
                };
            }

            _context.Blogs.Remove(existingBlog);
            await _context.SaveChangesAsync();

            return new ResponseDto()
            {
                IsSucceed = true,
                Message = "Blog deleted successfully"
            };

        }

        public async Task<List<Blog>> GetBlogsAsync()
        {
            return await _context.Blogs.OrderByDescending(n => n.CreateAt).ToListAsync();
        }

        public async Task<Blog?> GetDetailAsync(Guid id)
        {
            var existingBlog = await _context.Blogs.FindAsync(id);


            if (existingBlog == null)
            {
                return null;
            }

            return existingBlog;

        }

        public async Task<IResponse> UpdateAsync(Guid id, UpdateBlogDto blog)
        {
            var existingBlog = await _context.Blogs.FindAsync(id);

            if (existingBlog == null)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Blog not found"
                };
            }

            existingBlog.Title = blog.Title;
            existingBlog.Content = blog.Content;
            existingBlog.Thumbnail = blog.Thumbnail;
            existingBlog.UpdateAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new ResponseDto()
            {
                IsSucceed = true,
                Message = "Blog updated successfully"
            };

        }
    }
}