using System.Text.Json.Serialization;

namespace Product.Core.Dtos.Response
{
    public class ProfileResponse : IResponse
    {
        public bool IsSucceed { get; set; }

        public required Profile Profile { get; set; }

    }

    public class Profile
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string FullName { get; set; } = string.Empty;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Avatar { get; set; } = string.Empty;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string> Role { get; set; } = [];
    }
}