using Microsoft.AspNetCore.Mvc;
using Product.Core.Common.Validations;
using Product.Core.Domain.Dtos.Billboard;
using Product.Core.Interfaces;

namespace Product.Controllers
{
    [ApiController]
    [Route("api/product/billboard")]
    [ValidateModelState]
    public class BillboardController : ControllerBase
    {
        private readonly IBillboardService _billboardService;

        public BillboardController(IBillboardService billboardService)
        {
            _billboardService = billboardService;
        }

        [HttpPost]

        public async Task<IActionResult> CreateBillboard([FromForm] BillboardDto createBillboard, [FromForm] IFormFile file)
        {

            return Ok(await _billboardService.CreateAsync(createBillboard, file));

        }

        [HttpGet]

        public async Task<IActionResult> GetBillboards()
        {

            var result = await _billboardService.GetAsync();

            return Ok(result);
        }

        [HttpPut]
        [Route("{id}")]

        public async Task<IActionResult> UpdateBillboard([FromRoute] Guid id, [FromForm] BillboardDto updateBillboard, [FromForm] IFormFile file)
        {

            return Ok(await _billboardService.UpdateAsync(id, updateBillboard, file));
        }


        [HttpDelete]
        [Route("{id}")]

        public async Task<IActionResult> DeleteBillboard([FromRoute] Guid id)
        {

            return Ok(await _billboardService.DeleteAsync(id));
        }


        [HttpGet]
        [Route("{id}")]

        public async Task<IActionResult> GetDetailBillboard([FromRoute] Guid id)
        {

            return Ok(await _billboardService.GetDetailAsync(id));

        }
    }
}