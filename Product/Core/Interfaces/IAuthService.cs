using Product.Core.Dtos.Auth;
using Product.Core.Dtos.Response;

namespace Product.Core.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseDto> SeedRolesAsync();

        Task<ResponseDto> RegisterAsync(RegisterDto registerDto);

        Task<bool> IsExistsUserByEmail(string email);

        Task<IResponse> LoginAsync(LoginDto loginDto);

        Task<ResponseDto> MakeAdminAsync(UpdatePermissionDto updatePermissionDto);
        Task<ResponseDto> MakeManagerAsync(UpdatePermissionDto updatePermissionDto);

         Task<IResponse> SignInWithGoogleAsync(GoogleResponse googleResponse);

        Task<string> VerifyAccountAsync(string userId, string token);

        Task<string> ForgotPasswordAsync(string email);

        Task<string> ResetPasswordAsync(string userId, string token, string newPassword);

    }
}