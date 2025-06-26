using BLL.Helper;
using DAL.Entity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface ITokenBLL
    {
        string GenerateToken(User user, DateTime expires);
    }

    public class TokenBLL : ITokenBLL
    {
        public readonly IOptions<JWTSetting> appSettings;
        public TokenBLL(IOptions<JWTSetting> appSettings)
        {
            this.appSettings = appSettings;
        }

        public string GenerateToken(User user, DateTime expires)
        {
            var now = DateTime.UtcNow;
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var accessTokenKey = Encoding.ASCII.GetBytes(appSettings.Value.AccessTokenKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = appSettings.Value.Issuer,
                Audience = "",
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.UserName),
                    new Claim(ClaimTypes.GivenName,user.Id.ToString()),
                    new Claim(ClaimTypes.Role,user.RoleId.ToString()),
                    new Claim(ClaimTypes.Expiration,expires.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, now.ToString(), ClaimValueTypes.Integer64)
                }),
                NotBefore = DateTime.UtcNow,
                Expires = expires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(accessTokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var accessToken = jwtTokenHandler.WriteToken(token);
            return accessToken;
        }
    }
}
