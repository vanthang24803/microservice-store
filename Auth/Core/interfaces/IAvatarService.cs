using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth.Core.Dtos;

namespace Auth.Core.interfaces
{
    public interface IAvatarService
    {
        Task<ResponseDto> UploadAsync(string id, IFormFile file);
    }
}