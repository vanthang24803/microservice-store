using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Product.Core.Dtos.Response;

namespace Product.Core.Interfaces
{
    public class ResponseDto : IResponse
    {
        public bool IsSucceed { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Message { get; set; } = string.Empty;

    }
}