using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using RunGroops.Application.Models;
using RunGroops.Domain.EFModels;
using RunGroops.Domain.Enum;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;

namespace RunGroops.Application.Services
{
    public class JWTService : IJWTService
    {
        private readonly IConfiguration _configuration;

        public JWTService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(IdentityUser user, IList<string> roles, IList<Claim> claims)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.NameId, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.GivenName, user.UserName));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = _configuration["Token:Issuer"],
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}


