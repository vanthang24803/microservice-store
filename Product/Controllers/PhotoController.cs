using Microsoft.AspNetCore.Mvc;
using Product.Core.Common.Validations;
using Product.Core.Interfaces;

namespace Product.Controllers
{
    [ApiController]
    [Route("api/product")]
    [ValidateModelState]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoService _photoService;

        public PhotoController(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        [HttpPost]
        [Route("{id}/image")]
        public async Task<IActionResult> CreateImages([FromRoute] Guid id, List<IFormFile> files)
        {
            return Ok(await _photoService.CreateAsync(id, files));
        }

        [HttpDelete]
        [Route("{id}/image/{imageId}")]

        public async Task<IActionResult> DeleteImage([FromRoute] Guid id, [FromRoute] Guid imageId)
        {
            return Ok(await _photoService.DeleteAsync(id, imageId));
        }

        [HttpGet]
        [Route("{id}/images")]

        public async Task<IActionResult> GetImages([FromRoute] Guid id)
        {
            return Ok(await _photoService.GetAsync(id));
        }
    }
}