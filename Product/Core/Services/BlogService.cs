using System.Net;
using Microsoft.EntityFrameworkCore;
using Product.Context;
using Product.Core.Common.Utils;
using Product.Core.Dtos.Blogs;
using Product.Core.Interfaces;
using Product.Core.Models;
using Product.Core.Repositories;

namespace Product.Core.Services
{
    public class BlogService : IBlogService
    {
        private readonly ApplicationDbContext _context;

        private readonly IBlogRepository _blogRepository;

        public BlogService(ApplicationDbContext context, IBlogRepository blogRepository)
        {
            _context = context;
            _blogRepository = blogRepository;
        }

        public async Task<Response<Blog>> CreateAsync(BlogDto blogDto)
        {
            var result = await _blogRepository.Save(blogDto);
            return new Response<Blog>(HttpStatusCode.Created, result);
        }

        public async Task<string> DeleteAsync(Guid id)
        {

            return await _blogRepository.Delete(id);
        }

        public async Task<List<Blog>> GetBlogByAuthorAsync(Guid authorId)
        {
            return await _blogRepository.GetAllBlogs();
        }

        public async Task<List<Blog>> GetBlogsAsync()
        {
            return await _context.Blogs.OrderByDescending(n => n.CreateAt).ToListAsync();
        }

        public async Task<Blog> GetDetailAsync(Guid id)
        {
            return await _blogRepository.FindBlogById(id);
        }

        public async Task<Response<Blog>> UpdateAsync(Guid id, UpdateBlogDto blog)
        {

            var result = await _blogRepository.Update(id, blog);
            return new Response<Blog>(HttpStatusCode.OK, result);
        }
    }
}