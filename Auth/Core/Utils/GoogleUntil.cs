using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Auth.Core.Dtos;

namespace Auth.Core.Utils
{
    public class GoogleUntil
    {
        public static GoogleResponse CreateFromJwtToken(JwtSecurityToken jwtToken)
        {
            return new GoogleResponse
            {
                Email = jwtToken.Claims.First(c => c.Type == "email").Value,
                Name = jwtToken.Claims.First(c => c.Type == "name").Value,
                Avatar = jwtToken.Claims.First(c => c.Type == "picture").Value,
                FirstName = jwtToken.Claims.First(c => c.Type == "given_name").Value,
                LastName = jwtToken.Claims.First(c => c.Type == "family_name").Value,
            };
        }
    }
}