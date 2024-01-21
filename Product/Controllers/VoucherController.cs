using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Product.Core.Dtos.Voucher;
using Product.Core.Interfaces;

namespace Product.Controllers
{
    [ApiController]
    [Route("api/product/voucher")]
    public class VoucherController : ControllerBase
    {
        private readonly IVoucherService _voucherService;

        public VoucherController(IVoucherService voucherService)
        {
            _voucherService = voucherService;
        }

        [HttpPost]

        public async Task<IActionResult> CreateVoucher([FromBody] CreateVoucher createVoucher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _voucherService.CreateAsync(createVoucher);

            if (result.IsSucceed)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet]

        public async Task<IActionResult> GetVoucher()
        {
            var result = await _voucherService.GetAsync();

            return Ok(result);
        }

        [HttpPost]
        [Route("verify")]

        public async Task<IActionResult> VerifyVoucher()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _voucherService.CheckVoucherExpiryAsync();

            if (result.IsSucceed)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost]
        [Route("use-voucher")]

        public async Task<IActionResult> UseVoucher([FromBody] UseVoucher useVoucher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _voucherService.UseAsync(useVoucher);


            if (result.IsSucceed)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut]
        [Route("{id}")]

        public async Task<IActionResult> UpdateVoucher([FromRoute] Guid id, [FromBody] UpdateVoucher updateVoucher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _voucherService.UpdateAsync(id, updateVoucher);


            if (result.IsSucceed)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut]
        [Route("{id}/extend-voucher")]

        public async Task<IActionResult> ExtendVoucher([FromRoute] Guid id, [FromBody] ExtendVoucher extendVoucher)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _voucherService.ExtendAsync(id, extendVoucher);


            if (result.IsSucceed)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete]
        [Route("{id}")]

        public async Task<IActionResult> DeleteVoucher([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _voucherService.DeleteAsync(id);

            if (result.IsSucceed)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}