using Microsoft.AspNetCore.Mvc;
using Product.Core.Dtos.Information;
using Product.Core.Interfaces;

namespace Product.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class InformationController : ControllerBase
    {
        private readonly IInformationService _information;
        public InformationController(IInformationService information)
        {
            _information = information;
        }

        [HttpGet]
        [Route("{id}/information")]

        public async Task<IActionResult> GetInformation([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _information.GetAsync(id);

            if (result is null)
            {
                return NotFound("Information not found");
            }

            return Ok(result);
        }


        [HttpPost]
        [Route("{id}/information")]

        public async Task<IActionResult> CreateInformation([FromRoute] Guid id, [FromBody] CreateInformation createInformation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _information.CreateAsync(id, createInformation);

            if (result.IsSucceed)
            {
                return Ok(result);
            }

            return BadRequest(result);

        }

        [HttpDelete]
        [Route("{productId}/information/{id}")]

        public async Task<IActionResult> DeleteInformation([FromRoute] Guid productId, [FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _information.DeleteAsync(productId, id);

            if (result.IsSucceed)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut]
        [Route("{productId}/information/{id}")]

        public async Task<IActionResult> UpdateInformation([FromRoute] Guid productId, [FromRoute] Guid id, [FromBody] UpdateInformation updateInformation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _information.UpdateAsync(productId, id, updateInformation);

            if (result.IsSucceed)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}