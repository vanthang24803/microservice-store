using Microsoft.AspNetCore.Mvc;
using Product.Core.Dtos.Category;
using Product.Core.Interfaces;

namespace Product.Controllers
{
    [ApiController]
    [Route("api/product/category")]
    public class CategoryController : ControllerBase
    {
        public readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryRequest categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _categoryService.CreateAsync(categoryDto));
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteCategory([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(_categoryService.DeleteAsync(id));


        }

        [HttpGet]
        public async Task<IActionResult> GetListCategory()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _categoryService.GetAsync();

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]

        public async Task<IActionResult> GetDetailCategory([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _categoryService.GetDetailAsync(id);

            if (result is null)
            {
                return NotFound("Category not found");
            }
            return Ok(result);
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid id, [FromBody] CategoryRequest updateCategoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _categoryService.UpdateAsync(id, updateCategoryDto));
        }
    }
}