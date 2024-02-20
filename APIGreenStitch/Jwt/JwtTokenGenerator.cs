using APIGreenStitch.Models;
using Dtos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIGreenStitch.Jwt
{
    public class JwtTokenGenerator
    {
        private readonly IConfiguration _config;
        public JwtTokenGenerator(IConfiguration config)
        {
            _config = config;
        }
    

        public string GenerateTokenv1(MemberLoginDto user)
        {
            var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Email,user.Email),
                              new Claim(ClaimTypes.Role,"Member"),
                              new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),

                        };
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWTKey:Secret"]));
            var _TokenExpiryTimeInHour = Convert.ToInt64(_config["JWTKey:TokenExpiryTimeInHour"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _config["JWTKey:ValidIssuer"],
                Audience = _config["JWTKey:ValidAudience"],
               
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}