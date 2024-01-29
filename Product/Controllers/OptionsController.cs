using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Product.Core.Dtos.Option;
using Product.Core.Interfaces;

namespace Product.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class OptionsController : ControllerBase
    {
        private readonly IOptionsService _optionsService;

        public OptionsController(IOptionsService optionsService)
        {
            _optionsService = optionsService;
        }

        [HttpPost]
        [Route("{id}/options")]

        public async Task<IActionResult> CreateOptions([FromRoute] Guid id, CreateOptionsDto createOptionsDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _optionsService.CreateAsync(id, createOptionsDto);

            if (result.IsSucceed)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet]
        [Route("{id}/options")]

        public async Task<IActionResult> GetOptions([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _optionsService.GetAsync(id);

            return Ok(result);
        }

        [HttpPut]
        [Route("{id}/option/{optionId}")]

        public async Task<IActionResult> UpdateOption([FromRoute] Guid id, [FromRoute] Guid optionId, [FromBody] UpdateOptionsDto updateOptionsDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _optionsService.UpdateAsync(id, optionId, updateOptionsDto);

            if (result.IsSucceed)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete]
        [Route("{id}/option/{optionId}")]

        public async Task<IActionResult> DeleteOption([FromRoute] Guid id, [FromRoute] Guid optionId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _optionsService.DeleteAsync(id, optionId);

            if (result.IsSucceed)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet]
        [Route("{id}/option/{optionId}")]

        public async Task<IActionResult> GetDetailOption([FromRoute] Guid id, [FromRoute] Guid optionId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _optionsService.GetDetailAsync(id, optionId);

            if (result is null)
            {
                return NotFound("Option not found");
            }

            return Ok(result);
        }
    }
}