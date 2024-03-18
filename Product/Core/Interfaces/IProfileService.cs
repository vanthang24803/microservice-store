using Product.Core.Dtos.Auth;
using Product.Core.Dtos.Response;
using Product.Core.Models;

namespace Product.Core.Interfaces
{
    public interface IProfileService
    {
        Task<IResponse> GetProfileAsync(string id);

        Task<IResponse> UpdateProfileAsync(string id, UpdateProfileDto updateProfileDto);

        Task<ResponseDto> UpdatePasswordAsync(string id, UpdatePasswordDto updatePasswordDto);

        Task<IResponse> CreateAddressAsync(string id, AddressDto addressDto);

        Task<IResponse> UpdateAddressAsync(string id, Guid addressId, AddressDto addressDto);

        Task<List<Address>> FindAllAddressAsync(string id);

        Task<IResponse> DeleteAddressAsync(string id, Guid addressId);

        Task<IResponse> ActiveAddressAsync(string id, Guid addressId);


        Task<ResponseDto> UploadAvatarAsync(string id, IFormFile file);


    }
}