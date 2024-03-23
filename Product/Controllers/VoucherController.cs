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

        [HttpGet]
        [Route("{id}")]

        public async Task<IActionResult> FindVoucherById([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _voucherService.FindVoucherById(id);

            if (result != null)
            {
                return Ok(result);
            }

            return NotFound("Voucher not found");
        }

        [HttpPost]
        [Route("find")]

        public async Task<IActionResult> FindVoucher([FromBody] VoucherRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _voucherService.FindVoucherByCodeAsync(request);

            if (result.IsSucceed)
            {
                return Ok(result);
            }

            return NotFound(result);
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