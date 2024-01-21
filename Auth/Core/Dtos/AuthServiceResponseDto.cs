using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Auth.Core.Dtos
{
    public class AuthServiceResponseDto
    {
        public bool IsSucceed { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Message { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Token { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public List<string> Role { get; set; } = new List<string>();

         [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public UserDto User { get; set; } 

    }
}
