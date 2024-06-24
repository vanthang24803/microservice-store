using Newtonsoft.Json;

namespace Product.Core.Common.Utils
{
    public class ApiError
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; } = string.Empty;

        [JsonProperty("timestamp")]
        public DateTime Timestamp = DateTime.Now;
    }
}