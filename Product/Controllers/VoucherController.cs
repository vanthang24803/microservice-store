using Microsoft.AspNetCore.Mvc;
using Product.Core.Common.Validations;
using Product.Core.Dtos.Voucher;
using Product.Core.Interfaces;

namespace Product.Controllers
{
    [ApiController]
    [Route("api/product/voucher")]
    [ValidateModelState]
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
            return Ok(await _voucherService.FindVoucherByIdAsync(id));
        }

        [HttpPost]
        [Route("find")]

        public async Task<IActionResult> FindVoucher([FromBody] VoucherRequest request)
        {
            return Ok(await _voucherService.FindVoucherByCodeAsync(request));
        }


        [HttpPost]

        public async Task<IActionResult> CreateVoucher([FromBody] CreateVoucher createVoucher)
        {
            return Ok(await _voucherService.CreateAsync(createVoucher));
        }

        [HttpGet]

        public async Task<IActionResult> GetVoucher()
        {
            return Ok(await _voucherService.GetAsync());
        }

        [HttpPost]
        [Route("verify")]

        public async Task<IActionResult> VerifyVoucher()
        {
            return Ok(await _voucherService.CheckVoucherExpiryAsync());
        }

        [HttpPost]
        [Route("use-voucher")]

        public async Task<IActionResult> UseVoucher([FromBody] UseVoucher useVoucher)
        {
            return Ok(await _voucherService.UseAsync(useVoucher));

        }

        [HttpPut]
        [Route("{id}")]

        public async Task<IActionResult> UpdateVoucher([FromRoute] Guid id, [FromBody] UpdateVoucher updateVoucher)
        {
            return Ok(await _voucherService.UpdateAsync(id, updateVoucher));
        }

        [HttpPut]
        [Route("{id}/extend-voucher")]

        public async Task<IActionResult> ExtendVoucher([FromRoute] Guid id, [FromBody] ExtendVoucher extendVoucher)
        {

            return Ok(await _voucherService.ExtendAsync(id, extendVoucher));
        }

        [HttpDelete]
        [Route("{id}")]

        public async Task<IActionResult> DeleteVoucher([FromRoute] Guid id)
        {
            return Ok(await _voucherService.DeleteAsync(id));
        }
    }
}