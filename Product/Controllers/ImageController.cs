using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Product.Core.Interfaces;

namespace Product.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ImageController : ControllerBase
    {
        private readonly IPhotoService _photoService;

        public ImageController(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        [HttpPost]
        [Route("{id}/image")]

        public async Task<IActionResult> CreateImage([FromRoute] Guid id, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _photoService.CreateAsync(id, file);

            if (result.IsSucceed)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete]
        [Route("{id}/image/{imageId}")]

        public async Task<IActionResult> DeleteImage([FromRoute] Guid id, [FromRoute] Guid imageId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _photoService.DeleteAsync(id, imageId);

            if (result.IsSucceed)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet]
        [Route("{id}/images")]

        public async Task<IActionResult> GetImages([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _photoService.GetAsync(id);

            return Ok(result);
        }
    }
}