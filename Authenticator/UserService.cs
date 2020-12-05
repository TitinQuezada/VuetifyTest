using Core.Entities;
using Core.Interfaces;
using Core.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Authenticator
{
    public sealed class UserService : IUserService
    {
        private readonly string _secretKey;
        private readonly string _issuer;

        public UserService(string secretKey , string issuer)
        {
            _secretKey = secretKey;
            _issuer = issuer;
        }

        AuthenticationViewModel IUserService.GetToken(SystemUser user)
        {
            string token = generateJwtToken(user);

            return new AuthenticationViewModel(user, token);
        }

        private string generateJwtToken(SystemUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            var claims = new ClaimsIdentity(new[]
            {
                 new Claim("username" , user.Username),
                 new Claim(ClaimTypes.Name , user.Name),
                 new Claim("lastname" , user.Lastname),
                 new Claim(ClaimTypes.Email, user.Email),
            });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _issuer,
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
