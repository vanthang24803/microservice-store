using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth.Core.Dtos;

namespace Auth.Core.interfaces
{
    public interface IAuthService
    {
        Task<AuthServiceResponseDto> SeedRolesAsync();
        Task<AuthServiceResponseDto> RegisterAsync(RegisterDto registerDto);
        Task<AuthServiceResponseDto> LoginAsync(LoginDto loginDto);
        Task<AuthServiceResponseDto> MakeAdminAsync(UpdatePermissionDto updatePermissionDto);
        Task<AuthServiceResponseDto> MakeOwnerAsync(UpdatePermissionDto updatePermissionDto);

        Task<string> VerifyAccountAsync(string userId, string token);

        Task<string> ForgotPasswordAsync(string email);

        Task<string> ResetPasswordAsync(string userId , string token , string newPassword);
    }
}