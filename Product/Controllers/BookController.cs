using Microsoft.AspNetCore.Mvc;
using Product.Core.Dtos.Book;
using Product.Core.Interfaces;
using Product.Core.Utils;

namespace Product.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ICategoryProductService _categoryProduct;

        public BookController(IBookService bookService, ICategoryProductService categoryProduct)
        {
            _bookService = bookService;
            _categoryProduct = categoryProduct;
        }

        [HttpPut]
        [Route("{id}/detail")]

        public async Task<IActionResult> UpdateDetailBook(
         [FromRoute] Guid id, [FromBody] DetailDto detail
        )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _bookService.UpdateDetailAsync(id, detail);

            if (result.IsSucceed)
            {
                return Ok(result);
            }

            return BadRequest(result);

        }

        [HttpGet]
        [Route("selling")]

        public async Task<IActionResult> GetSellingBooks()
        {
            var result = await _bookService.GetBooksSelling();
            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _bookService.GetAsync(query);

            return Ok(result);

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookDto createBookDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _bookService.CreateAsync(createBookDto);

            if (result.IsSucceed)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateBookDto updateBookDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _bookService.UpdateAsync(id, updateBookDto);

            if (result.IsSucceed)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetDetail([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _bookService.GetDetailAsync(id);

            if (result is null)
            {
                return NotFound("Product not found");
            }

            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _bookService.DeleteAsync(id);

            if (result.IsSucceed)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


        [HttpPost]
        [Route("{id}/category/{categoryId}")]
        public async Task<IActionResult> CreateCategoryProduct([FromRoute] Guid id, [FromRoute] Guid categoryId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _categoryProduct.AddCategoryToProduct(id, categoryId);

            if (result.IsSucceed)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete]
        [Route("{id}/category/{categoryId}")]
        public async Task<IActionResult> RemoveCategoryProduct([FromRoute] Guid id, [FromRoute] Guid categoryId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _categoryProduct.DeleteCategoryToProduct(id, categoryId);

            if (result.IsSucceed)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet]
        [Route("total")]
        public async Task<IActionResult> GetTotalProduct()
        {
            var result = await _bookService.GetTotalProduct();
            return Ok(result);
        }
    }
}