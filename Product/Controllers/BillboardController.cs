using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Product.Core.Dtos.Billboard;
using Product.Core.Interfaces;

namespace Product.Controllers
{
    [ApiController]
    [Route("api/product/billboard")]
    public class BillboardController : ControllerBase
    {
        private readonly IBillboardService _billboardService;

        public BillboardController(IBillboardService billboardService)
        {
            _billboardService = billboardService;
        }

        [HttpPost]

        public async Task<IActionResult> CreateBillboard([FromForm] CreateBillboard createBillboard, [FromForm] IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _billboardService.CreateAsync(createBillboard, file);

            if (result.IsSucceed)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet]

        public async Task<IActionResult> GetBillboards()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _billboardService.GetAsync();

            return Ok(result);
        }

        [HttpPut]
        [Route("{id}")]

        public async Task<IActionResult> UpdateBillboard([FromRoute] Guid id, [FromForm] UpdateBillboard updateBillboard, [FromForm] IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _billboardService.UpdateAsync(id, updateBillboard, file);

            if (result.IsSucceed)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


        [HttpDelete]
        [Route("{id}")]

        public async Task<IActionResult> DeleteBillboard([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _billboardService.DeleteAsync(id);

            if (result.IsSucceed)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

    }
}