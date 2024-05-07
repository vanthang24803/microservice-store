using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Product.Core.Dtos.Auth;
using Product.Core.Interfaces;
using Product.Core.Utils;

namespace Product.Controllers
{
    [Route("/api/auth")]
    public class AuthController : Controller
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
            var isExistsUser = await _authService.IsExistsUserByEmail(registerDto.Email);

            if (isExistsUser)
            {
                return BadRequest("This email has created an account!");
            }

            var registerResult = await _authService.RegisterAsync(registerDto);

            if (registerResult.IsSucceed)
                return Ok(registerResult);

            return BadRequest(registerResult);
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

        [HttpGet]
        [Route("verify-account")]

        public async Task<IActionResult> VerifyEmail(
             [FromQuery] string userId, [FromQuery] string token)

        {
            var message = await _authService.VerifyAccountAsync(userId, token);

            if (!message.IsSucceed)
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
        [Route("social")]

        public async Task<IActionResult> SignInWithGoogle([FromQuery] string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            var user = new SocialRequest()
            {
                Email = jwtToken.Payload.Claims.First(c => c.Type == "email").Value,
                Name = jwtToken.Payload.Claims.First(c => c.Type == "name").Value,
                Avatar = jwtToken.Payload.Claims.First(c => c.Type == "picture").Value,

            };

            var result = await _authService.SocialSignInAsync(user);

            if (!result.IsSucceed)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }


    }
}