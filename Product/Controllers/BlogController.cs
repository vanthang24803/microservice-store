using Microsoft.AspNetCore.Mvc;
using Product.Core.Dtos.Blogs;
using Product.Core.Interfaces;

namespace Product.Controllers
{
    [ApiController]
    [Route("api/blog")]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        [Route("author/{id}")]

        public async Task<IActionResult> GetBlogByAuthor(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _blogService.GetBlogByAuthorAsync(id);

            return Ok(result);
        }

        [HttpPut]
        [Route("{id}")]

        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateBlogDto update)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _blogService.UpdateAsync(id, update);

            if (result.IsSucceed)
            {
                return Ok(result);
            }

            return NotFound(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BlogDto blogDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _blogService.CreateAsync(blogDto);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> FindAll()
        {
            var result = await _blogService.GetBlogsAsync();

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]

        public async Task<IActionResult> FindDetail([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _blogService.GetDetailAsync(id);

            if (result == null)
            {
                return NotFound("Blog not found");
            }

            return Ok(result);

        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Remove([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _blogService.DeleteAsync(id);

            if (result.IsSucceed)
            {
                return Ok(result);
            }

            return NotFound(result);
        }


    }

}