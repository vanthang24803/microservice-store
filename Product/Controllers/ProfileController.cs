using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Product.Core.Dtos.Auth;
using Product.Core.Interfaces;

namespace Product.Controllers
{
    [ApiController]
    [Route("api/auth/profile")]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        private readonly IAuthService _authService;

        public ProfileController(IProfileService profileService, IAuthService authService)
        {
            _profileService = profileService;
            _authService = authService;
        }

        [HttpGet]
        [Authorize]
        [Route("{id}")]
        public async Task<IActionResult> GetProfile([FromRoute] string id)
        {
            var profileResult = await _profileService.GetProfileAsync(id);

            if (profileResult.IsSucceed)
            {
                return Ok(profileResult);
            }

            return BadRequest(profileResult);
        }

        [HttpPut]
        // [Authorize]
        [Route("{id}")]
        public async Task<IActionResult> UpdateProfile([FromRoute] string id, [FromBody] UpdateProfileDto updateProfileDto)
        {
            var updateResult = await _profileService.UpdateProfileAsync(id, updateProfileDto);
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
            var updatePasswordResult = await _profileService.UpdatePasswordAsync(id, updatePasswordDto);
            if (updatePasswordResult.IsSucceed)
            {
                return Ok(updatePasswordResult);
            }

            return BadRequest(updatePasswordResult);

        }


        [HttpPost]
        [Route("{id}/address")]

        public async Task<IActionResult> CreateAddress([FromRoute] string id, [FromBody] AddressDto addressDto)
        {
            var result = await _profileService.CreateAddressAsync(id, addressDto);

            if (result.IsSucceed)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet]
        [Route("{id}/address")]

        public async Task<IActionResult> FindAllAddress([FromRoute] string id)
        {
            var exitingUser = await _authService.IsExistsUserById(id);
            if (!exitingUser)
            {
                return BadRequest("User not found");
            }

            var result = await _profileService.FindAllAddressAsync(id);

            return Ok(result);
        }

        [HttpPut]
        [Route("{id}/address/{addressId}")]
        public async Task<IActionResult> UpdateAddress([FromRoute] string id, [FromRoute] Guid addressId, [FromBody] AddressDto addressDto)
        {
            var result = await _profileService.UpdateAddressAsync(id, addressId, addressDto);

            if (result.IsSucceed)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete]
        [Route("{id}/address/{addressId}")]
        public async Task<IActionResult> DeleteAddress([FromRoute] string id, [FromRoute] Guid addressId)
        {
            var result = await _profileService.DeleteAddressAsync(id, addressId);

            if (result.IsSucceed)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost]
        [Route("{id}/avatar")]
        public async Task<IActionResult> UploadAvatar([FromRoute] string id, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _profileService.UploadAvatarAsync(id, file);

            if (result.IsSucceed)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}