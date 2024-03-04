using System.IdentityModel.Tokens.Jwt;
using Auth.Core.Constant;
using Auth.Core.Dtos;
using Auth.Core.interfaces;
using Auth.Core.Utils;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Auth.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;


        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("seed-roles")]
        public async Task<IActionResult> SeedRoles()
        {
            var seerRoles = await _authService.SeedRolesAsync();

            return Ok(seerRoles);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var registerResult = await _authService.RegisterAsync(registerDto);

            if (registerResult.IsSucceed)
                return Ok(registerResult);

            return BadRequest(registerResult);
        }

        [HttpGet]
        [Route("verify-account")]

        public async Task<IActionResult> VerifyEmail(
             [FromQuery] string userId, [FromQuery] string token)

        {
            var message = await _authService.VerifyAccountAsync(userId, token);

            if (message == "Account not found")
            {
                return NotFound(message);
            }

            if (message == "Account verification failed.")
            {
                return BadRequest(message);
            }

            return Ok(message);
        }

        [HttpPost]
        [Route("forgot-password")]

        public async Task<IActionResult> ForgotPassword(
            [FromBody] ForgotPasswordDto forgot
        )
        {
            var message = await _authService.ForgotPasswordAsync(forgot.Email);

            if (message == "Account not found")
            {
                return NotFound(message);
            }

            if (message == "Password reset failed.")
            {
                return BadRequest(message);
            }

            return Ok(message);
        }

        [HttpPost]
        [Route("reset-password")]

        public async Task<IActionResult> ResetPassword(
            [FromQuery] string userId, [FromQuery] string token, [FromBody] ResetPasswordDto resetPassword
        )
        {
            var message = await _authService.ResetPasswordAsync(userId, token, resetPassword.NewPassword);

            if (message == "Account not found")
            {
                return NotFound(message);
            }

            if (message == "Password reset failed.")
            {
                return BadRequest(message);
            }

            return Ok(message);

        }


        [HttpPost]
        [Route("login")]

        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var loginResult = await _authService.LoginAsync(loginDto);
            if (loginResult.IsSucceed)
                return Ok(loginResult);

            return BadRequest(loginResult);

        }

        [HttpPost]
        [Authorize(Roles = StaticUserRole.USER)]
        [Route("make-admin")]
        public async Task<IActionResult> MakeAdmin([FromBody] UpdatePermissionDto updatePermissionDto)
        {
            var operationResult = await _authService.MakeAdminAsync(updatePermissionDto);

            if (operationResult.IsSucceed)
                return Ok(operationResult);

            return BadRequest(operationResult);
        }

        [HttpPost]
        [Authorize(Roles = StaticUserRole.USER)]
        [Route("make-manager")]
        public async Task<IActionResult> MakeManager([FromBody] UpdatePermissionDto updatePermissionDto)
        {
            var operationResult = await _authService.MakeManagerAsync(updatePermissionDto);

            if (operationResult.IsSucceed)
                return Ok(operationResult);

            return BadRequest(operationResult);
        }

        [HttpPost]
        [Route("google")]
        public async Task<IActionResult> SignInWithGoogle([FromQuery] string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            var user = GoogleUntil.CreateFromJwtToken(jwtToken);

            if (user != null)
            {
                var message = await _authService.SignInWithGoogleAsync(user);

                if (!message.IsSucceed)
                {
                    return BadRequest(message);
                }

                return Ok(message);

            }
            else
            {
                return BadRequest("Token is valid");
            }
        }

    }
}