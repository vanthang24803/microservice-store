

using System.IdentityModel.Tokens.Jwt;
using Product.Core.Dtos.Auth;

namespace Product.Core.Utils
{
    public class SocialUntil
    {
        public SocialRequest CreateFromJwtToken(JwtSecurityToken jwtToken)
        {
            return new SocialRequest
            {
                Email = jwtToken.Claims.First(c => c.Type == "email").Value,
                Name = jwtToken.Claims.First(c => c.Type == "name").Value,
                Avatar = jwtToken.Claims.First(c => c.Type == "picture").Value,
            };
        }
    }
}