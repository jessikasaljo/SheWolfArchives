﻿using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SheWolf.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SheWolf.Application.Queries.Users.Login.Helpers
{
    public class TokenHelper
    {
        private readonly IConfiguration configuration;

        public TokenHelper(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public string GenerateJwtToken(User user)
        {
            var key = Encoding.ASCII.GetBytes(s: configuration["JwtSettings:SecretKey"]!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, "Admin")
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
