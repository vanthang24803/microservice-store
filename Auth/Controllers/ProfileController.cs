using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth.Core.Dtos;
using Auth.Core.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers
{
    [ApiController]
    [Route("api/auth/profile")]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet]
        [Authorize]
        [Route("{id}")]
        public async Task<IActionResult> GetProfile([FromRoute] string id)
        {
            var profileResult = await _profileService.GetProfile(id);

            if (profileResult.IsSucceed)
            {
                return Ok(profileResult);
            }

            return BadRequest(profileResult);
        }

        [HttpPut]
        [Authorize]
        [Route("{id}")]
        public async Task<IActionResult> UpdateProfile([FromRoute] string id, [FromBody] UpdateProfileDto updateProfileDto)
        {
            var updateResult = await _profileService.UpdateProfile(id, updateProfileDto);
            if (updateResult.IsSucceed)
            {
                return Ok(updateResult);
            }

            return BadRequest(updateResult);
        }

        [HttpPut]
        [Authorize]
        [Route("{id}/password")]
        public async Task<IActionResult> UpdatePassword([FromRoute] string id, [FromBody] UpdatePasswordDto updatePasswordDto)
        {
            var updatePasswordResult = await _profileService.UpdatePassword(id, updatePasswordDto);
            if (updatePasswordResult.IsSucceed)
            {
                return Ok(updatePasswordResult);
            }

            return BadRequest(updatePasswordResult);

        }
    }
}