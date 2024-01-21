using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth.Core.Dtos;

namespace Auth.Core.interfaces
{
    public interface IProfileService
    {
        Task<ProfileServiceResponseDto> GetProfile(string id);

        Task<UpdateResponseDto> UpdateProfile(string id, UpdateProfileDto updateProfileDto);

        Task<ResponseDto> UpdatePassword(string id, UpdatePasswordDto updatePasswordDto);

    }
}