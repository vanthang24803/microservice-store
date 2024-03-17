using Product.Core.Dtos.Response;

namespace Product.Core.Dtos.Auth
{
    public class LoginResponse : IResponse
    {
        public bool IsSucceed { get; set; }

        public string Token { get; set; } = string.Empty;

        public required User User { get; set; }
    }


    public class User
    {
        public string Id { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Avatar { get; set; } = string.Empty;

        public IList<string> Role { get; set; } = [];
    }
}