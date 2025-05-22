using MassTransit;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Commands.RefreshTokens;
using WorkshopHub.Domain.Commands.Users.UpdateUser;
using WorkshopHub.Domain.Entities;
using WorkshopHub.Domain.Interfaces;
using WorkshopHub.Domain.Settings;

namespace WorkshopHub.Domain.Helpers
{
    public static class TokenHelper
    {
        public static string BuildToken(User user, TokenSettings tokenSettings, double _expiryDurationMinutes)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName)
            };

            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(tokenSettings.Secret));

            var credentials = new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new JwtSecurityToken(
                tokenSettings.Issuer,
                tokenSettings.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(_expiryDurationMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        public static async Task<string> GenerateRefreshToken(Guid userId, IMediatorHandler bus, int _expiryDurationDays)
        {
            var randomNumber = new Byte[32];
            var randomNumberGenerator = RandomNumberGenerator.Create();

            randomNumberGenerator.GetBytes(randomNumber);
            string token = Convert.ToBase64String(randomNumber);

            await bus.SendCommandAsync(new CreateRefreshTokenCommand(
               Guid.NewGuid(),
               userId,
               token,
               TimeHelper.GetTimeNow().AddDays(_expiryDurationDays)
            ));

            return token;
        }

        public static string Generate6DigitToken()
        {
            var rng = new Random();
            int token = rng.Next(100000, 999999);
            return token.ToString();
        }
    }
}
