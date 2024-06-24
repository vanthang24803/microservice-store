using System.Net;
using Product.Core.Common.Exceptions;
using Product.Core.Common.Utils;
using Product.Core.Dtos.Category;
using Product.Core.Interfaces;
using Product.Core.Models;
using Product.Core.Repositories;

namespace Product.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Response<Category>> CreateAsync(CategoryRequest request)
        {
            var result = await _categoryRepository.Save(request);
            return new Response<Category>(HttpStatusCode.Created, result);
        }

        public async Task<string> DeleteAsync(Guid id)
        {
            var result = await _categoryRepository.Delete(id);

            if (!result) throw new BadRequestException();

            return "OK";
        }

        public async Task<List<Category>> GetAsync()
        {
            return await _categoryRepository.FindAll();
        }

        public async Task<Response<Category>> UpdateAsync(Guid id, CategoryRequest update)
        {
            var result = await _categoryRepository.Update(id, update);

            return new Response<Category>(HttpStatusCode.OK, result);
        }

        public async Task<Category> GetDetailAsync(Guid id)
        {
            return await _categoryRepository.GetDetail(id);
        }
    }
}