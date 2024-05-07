using Product.Core.Dtos.Auth;
using Product.Core.Dtos.Response;

namespace Product.Core.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseDto> SeedRolesAsync();

        Task<ResponseDto> RegisterAsync(RegisterDto registerDto);

        Task<bool> IsExistsUserByEmail(string email);
        Task<bool> IsExistsUserById(string id);

        Task<IResponse> LoginAsync(LoginDto loginDto);

        Task<ResponseDto> MakeAdminAsync(UpdatePermissionDto updatePermissionDto);
        Task<ResponseDto> MakeManagerAsync(UpdatePermissionDto updatePermissionDto);

        Task<IResponse> SocialSignInAsync(SocialRequest socialRequest);

        Task<IResponse> VerifyAccountAsync(string userId, string token);

        Task<string> ForgotPasswordAsync(string email);

        Task<string> ResetPasswordAsync(string userId, string token, string newPassword);

    }
}