using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Product.Core.Utils
{
    public class TokenUtils
    {
        private readonly IConfiguration _configuration;

        public TokenUtils(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateNewJsonWebToken(List<Claim> claims)
        {
            var authSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var tokenObject = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddMonths(1),
                    claims: claims,
                    signingCredentials: new SigningCredentials(authSecret, SecurityAlgorithms.HmacSha256)
                );

            string token = new JwtSecurityTokenHandler().WriteToken(tokenObject);

            return token;
        }
    }
}