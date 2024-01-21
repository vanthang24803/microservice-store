using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth.Core.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AvatarController : ControllerBase
    {
        private readonly IAvatarService _avatar;

        public AvatarController(IAvatarService avatar)
        {
            _avatar = avatar;
        }

        [Authorize]
        [HttpPost]
        [Route("{id}/avatar")]
        public async Task<IActionResult> UploadAvatar([FromRoute] string id, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _avatar.UploadAsync(id, file);

            if (result.IsSucceed)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}