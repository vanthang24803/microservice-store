using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Product.Core.Dtos.Auth;
using Product.Core.Dtos.Response;
using Product.Core.Interfaces;
using Product.Core.Models;
using Product.Core.Utils;

namespace Product.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        private readonly IMailService _mailService;
        private readonly TokenUtils tokenUtils;
        readonly string _client = "http://localhost:3000";


        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IMailService mailService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _mailService = mailService;
            tokenUtils = new TokenUtils(_configuration);
        }


        public async Task<bool> IsExistsUserByEmail(string email)
        {
            var isExistsUser = await _userManager.FindByEmailAsync(email);
            return isExistsUser != null;
        }


        public async Task<ResponseDto> MakeAdminAsync(UpdatePermissionDto updatePermissionDto)
        {
            var user = await _userManager.FindByEmailAsync(updatePermissionDto.Email);

            if (user is null)
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Invalid User name!!!!!!!!"
                };

            await _userManager.AddToRoleAsync(user, StaticUserRole.ADMIN);

            return new ResponseDto()
            {
                IsSucceed = true,
                Message = $"{user.Email} is Admin",
            };
        }

        public async Task<ResponseDto> MakeManagerAsync(UpdatePermissionDto updatePermissionDto)
        {
            var user = await _userManager.FindByEmailAsync(updatePermissionDto.Email);

            if (user is null)
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Invalid User name!!!!!!!!"
                };

            await _userManager.AddToRoleAsync(user, StaticUserRole.MANAGER);

            return new ResponseDto()
            {
                IsSucceed = true,
                Message = $"{user.Email} is Manager",
            };
        }

        public async Task<ResponseDto> RegisterAsync(RegisterDto registerDto)
        {

            ApplicationUser newUser = new ApplicationUser()
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                UserName = registerDto.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                Avatar = string.Empty,
            };

            var createUserResult = await _userManager.CreateAsync(newUser, registerDto.Password);

            if (!createUserResult.Succeeded)
            {
                var errorString = "User Creation Failed Beacause: ";
                foreach (var error in createUserResult.Errors)
                {
                    errorString += " # " + error.Description;
                }
                return new ResponseDto()
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
                    Message = "<a href='" + _client + "/verify-account?userId=" + newUser.Id + "&token=" + token
                    + "' target='_blank'>Click here to verify your account</a>"

                };
                await _mailService.SendEmailAsync(mailRequest);

                return new ResponseDto()
                {
                    IsSucceed = true,
                    Message = "User Created Successfully"
                };

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Mail Send Error"
                };
            }
        }


        public async Task<ResponseDto> SeedRolesAsync()
        {
            bool isManageRoleExits = await _roleManager.RoleExistsAsync(StaticUserRole.MANAGER);
            bool isAdminRoleExists = await _roleManager.RoleExistsAsync(StaticUserRole.ADMIN);
            bool isUserRoleExists = await _roleManager.RoleExistsAsync(StaticUserRole.USER);

            if (isManageRoleExits && isAdminRoleExists && isUserRoleExists)
                return new ResponseDto()
                {
                    IsSucceed = true,
                    Message = "Roles Seeding is Already Done"
                };

            await _roleManager.CreateAsync(new IdentityRole(StaticUserRole.USER));
            await _roleManager.CreateAsync(new IdentityRole(StaticUserRole.ADMIN));
            await _roleManager.CreateAsync(new IdentityRole(StaticUserRole.MANAGER));

            return new ResponseDto()
            {
                IsSucceed = true,
                Message = "Role Seeding Done Successfully"
            };
        }

        public async Task<IResponse> VerifyAccountAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Email not found",
                };
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
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

                var tokenLogin = tokenUtils.GenerateNewJsonWebToken(authClaims);

                return new LoginResponse()
                {
                    IsSucceed = true,
                    Token = tokenLogin,
                    User = new User
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Name = $"{user.FirstName} {user.LastName}",
                        Avatar = user.Avatar,
                        Role = userRoles,
                    }
                };
            }

            return new ResponseDto()
            {
                IsSucceed = false,
                Message = "Verify error",
            };
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
                    Message = "<a href='" + _client + "/reset-password?userId=" + isExistsUser.Id + "&token=" + token
                    + "' target='_blank'>Click here to reset password</a>"

                };
                await _mailService.SendEmailAsync(mailRequest);

                return "Token Send Success";


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "Mail Send Error";
            }
        }

        public async Task<IResponse> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.Email);

            if (user is null)
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Invalid Credentials",
                };

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, loginDto.Password);


            if (!isPasswordCorrect)
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "Invalid Credentials",
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

            return new LoginResponse()
            {
                IsSucceed = true,
                Token = token,
                User = new User
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = $"{user.FirstName} {user.LastName}",
                    Avatar = user.Avatar,
                    Role = userRoles,
                }
            };
        }

        public async Task<bool> IsExistsUserById(string id)
        {
            var existingUser = await _userManager.FindByIdAsync(id);

            return existingUser != null;

        }

        public async Task<IResponse> SocialSignInAsync(SocialRequest socialRequest)
        {
            var user = await _userManager.FindByNameAsync(socialRequest.Email);

            if (user is null)
            {
                ApplicationUser newUser = new ApplicationUser()
                {
                    SecurityStamp = Guid.NewGuid().ToString(),
                    LastName = socialRequest.Name,
                    Email = socialRequest.Email,
                    UserName = socialRequest.Email,
                    Avatar = socialRequest.Avatar,
                    EmailConfirmed = true,
                };

                var createUserResult = await _userManager.CreateAsync(newUser);
                if (!createUserResult.Succeeded)
                {
                    var errorString = "User Creation Failed Because: ";
                    foreach (var error in createUserResult.Errors)
                    {
                        errorString += " # " + error.Description;
                    }
                    return new ResponseDto()
                    {
                        IsSucceed = false,
                        Message = errorString
                    };
                }

                await _userManager.AddToRoleAsync(newUser, StaticUserRole.USER);

                var userRoles = await _userManager.GetRolesAsync(newUser);

                var authClaims = new List<Claim>
                {
                    new(ClaimTypes.Name, newUser.UserName),
                    new(ClaimTypes.NameIdentifier, newUser.Id),
                    new("JWTID", Guid.NewGuid().ToString()),
                    new("FirstName", newUser.FirstName),
                    new("Avatar" , newUser.Avatar),
                    new("LastName", newUser.LastName),
                    new("Email" , newUser.Email ),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    authClaims.Add(new Claim("Role", userRole));
                }

                var token = tokenUtils.GenerateNewJsonWebToken(authClaims);

                return new LoginResponse()
                {
                    IsSucceed = true,
                    Token = token,
                    User = new User
                    {
                        Id = newUser.Id,
                        Email = newUser.Email,
                        Name = $"{newUser.FirstName} {newUser.LastName}",
                        Avatar = newUser.Avatar,
                        Role = userRoles,
                    }
                };
            }

            else
            {
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

                return new LoginResponse()
                {
                    IsSucceed = true,
                    Token = token,
                    User = new User
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Name = $"{user.FirstName} {user.LastName}",
                        Avatar = user.Avatar,
                        Role = userRoles,
                    }
                };
            }
        }
    }
}