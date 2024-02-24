using System.Security.Claims;
using Auth.Core.Constant;
using Auth.Core.Dtos;
using Auth.Core.interfaces;
using Auth.Core.Models;
using Auth.Core.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Auth.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        private readonly IGmailService _gmailService;

        private readonly TokenUtils tokenUtils;

        private readonly Client _client;

        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IGmailService gmailService, IOptions<Client> clientOptions)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _gmailService = gmailService;
            _client = clientOptions.Value;
            tokenUtils = new TokenUtils(_configuration);
        }


        public async Task<AuthServiceResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Email);

            if (user is null)
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "Invalid Credentials",
                    Role = null,
                };

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!isPasswordCorrect)
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "Invalid Credentials",
                    Role = null,
                };

            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new(ClaimTypes.Name, user.UserName),
                new(ClaimTypes.NameIdentifier, user.Id),
                new("JWTID", Guid.NewGuid().ToString()),
                new("FirstName", user.FirstName),
                new("Avatar" , user.Avatar),
                new("LastName", user.LastName),
                new("Email" , user.Email ),
        };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                authClaims.Add(new Claim("Role", userRole));
            }

            var token = tokenUtils.GenerateNewJsonWebToken(authClaims);

            return new AuthServiceResponseDto()
            {
                IsSucceed = true,
                Token = token,
                User = new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = $"{user.FirstName} {user.LastName}",
                    Avatar = user.Avatar,
                    Role = userRoles,
                }
            };

        }

        public async Task<AuthServiceResponseDto> MakeAdminAsync(UpdatePermissionDto updatePermissionDto)
        {
            var user = await _userManager.FindByEmailAsync(updatePermissionDto.Email);

            if (user is null)
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "Invalid User name!!!!!!!!"
                };

            await _userManager.AddToRoleAsync(user, StaticUserRole.ADMIN);

            return new AuthServiceResponseDto()
            {
                IsSucceed = true,
                Message = $"{user.Email} is Admin",
            };
        }

        public Task<AuthServiceResponseDto> MakeOwnerAsync(UpdatePermissionDto updatePermissionDto)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthServiceResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            var isExistsUser = await _userManager.FindByEmailAsync(registerDto.Email);

            if (isExistsUser != null)
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "UserName Already Exists"
                };

            ApplicationUser newUser = new ApplicationUser()
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                UserName = registerDto.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                Avatar = string.Empty,
                Address = string.Empty,
            };

            var createUserResult = await _userManager.CreateAsync(newUser, registerDto.Password);

            if (!createUserResult.Succeeded)
            {
                var errorString = "User Creation Failed Beacause: ";
                foreach (var error in createUserResult.Errors)
                {
                    errorString += " # " + error.Description;
                }
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = errorString
                };
            }

            await _userManager.AddToRoleAsync(newUser, StaticUserRole.USER);

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

            try
            {
                MailRequest mailRequest = new()
                {
                    ToEmail = registerDto.Email,
                    Subject = "Verify account",
                    Message = "<a href='" + _client.Url + "/verify-account?userId=" + newUser.Id + "&token=" + token
                    + "' target='_blank'>Click here to verify your account</a>"

                };
                await _gmailService.SendEmailAsync(mailRequest);

                return new AuthServiceResponseDto()
                {
                    IsSucceed = true,
                    Message = "User Created Successfully"
                };

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new AuthServiceResponseDto()
                {
                    IsSucceed = false,
                    Message = "Mail Send Error"
                };
            }
        }

        public async Task<string> ForgotPasswordAsync(string email)
        {
            var isExistsUser = await _userManager.FindByEmailAsync(email);

            if (isExistsUser == null)
            {
                return "Email not found";
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(isExistsUser);

            try
            {
                MailRequest mailRequest = new()
                {
                    ToEmail = email,
                    Subject = "Reset password",
                    Message = "<a href='" + _client.Url + "/reset-password?userId=" + isExistsUser.Id + "&token=" + token
                    + "' target='_blank'>Click here to reset password</a>"

                };
                await _gmailService.SendEmailAsync(mailRequest);

                return "Token Send Success";


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "Mail Send Error";
            }

        }


        public async Task<string> ResetPasswordAsync(string userId, string token, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return "Account not found";
            }

            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            if (result.Succeeded)
            {
                return "Password reset successfully";
            }

            return "Password reset failed.";
        }


        public async Task<string> VerifyAccountAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return "Account not found";
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                return "Account verified successfully";
            }

            return "Account verification failed.";
        }

        public async Task<AuthServiceResponseDto> SeedRolesAsync()
        {
            bool isOwnerRoleExists = await _roleManager.RoleExistsAsync(StaticUserRole.OWNER);
            bool isAdminRoleExists = await _roleManager.RoleExistsAsync(StaticUserRole.ADMIN);
            bool isUserRoleExists = await _roleManager.RoleExistsAsync(StaticUserRole.USER);

            if (isOwnerRoleExists && isAdminRoleExists && isUserRoleExists)
                return new AuthServiceResponseDto()
                {
                    IsSucceed = true,
                    Message = "Roles Seeding is Already Done"
                };

            await _roleManager.CreateAsync(new IdentityRole(StaticUserRole.USER));
            await _roleManager.CreateAsync(new IdentityRole(StaticUserRole.ADMIN));
            await _roleManager.CreateAsync(new IdentityRole(StaticUserRole.OWNER));

            return new AuthServiceResponseDto()
            {
                IsSucceed = true,
                Message = "Role Seeding Done Successfully"
            };
        }
    }

}