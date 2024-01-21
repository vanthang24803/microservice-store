using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth.Core.Context;
using Auth.Core.Dtos;
using Auth.Core.interfaces;
using Auth.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Auth.Core.Services
{
    public class AvatarService : IAvatarService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUploadService _upload;

        public AvatarService(UserManager<ApplicationUser> userManager, IUploadService upload)
        {
            _userManager = userManager;
            _upload = upload;
        }

        public async Task<ResponseDto> UploadAsync(string id, IFormFile file)
        {
            var exitingUser = await _userManager.FindByIdAsync(id);

            if (exitingUser is null)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = "User not found"
                };
            }

            var result = await _upload.AddPhotoAsync(file);

            if (result.Error != null)
            {
                return new ResponseDto()
                {
                    IsSucceed = false,
                    Message = result.Error.Message
                };
            }

            exitingUser.Avatar = result.SecureUrl.AbsoluteUri;

            await _userManager.UpdateAsync(exitingUser);

            return new ResponseDto()
            {
                IsSucceed = true,
                Message = "Avatar upload successfully"
            };


        }
    }
}