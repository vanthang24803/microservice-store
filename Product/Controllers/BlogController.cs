using Microsoft.AspNetCore.Mvc;
using Product.Core.Common.Validations;
using Product.Core.Dtos.Blogs;
using Product.Core.Interfaces;

namespace Product.Controllers
{
    [ApiController]
    [Route("api/blog")]
    [ValidateModelState]
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
            return Ok(await _blogService.GetBlogByAuthorAsync(id));
        }

        [HttpPut]
        [Route("{id}")]

        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateBlogDto update)
        {

            return Ok(await _blogService.UpdateAsync(id, update));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BlogDto blogDto)
        {


            return Ok(await _blogService.CreateAsync(blogDto));
        }

        [HttpGet]
        public async Task<IActionResult> FindAll()
        {

            return Ok(await _blogService.GetBlogsAsync());
        }

        [HttpGet]
        [Route("{id}")]

        public async Task<IActionResult> FindDetail([FromRoute] Guid id)
        {



            return Ok(await _blogService.GetDetailAsync(id));

        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Remove([FromRoute] Guid id)
        {
            return Ok(await _blogService.DeleteAsync(id));
        }
    }

}