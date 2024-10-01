using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnlineStore.Infrastructure.JwtGenerator
{
    /// <summary>
    /// Интерфейс сервиса генерации токенов.
    /// </summary>
    public sealed class JwtGenerator : IJwtGenerator
    {
        /// <inheritdoc/>
        public string GenerateToken()
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "ApiClient"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("In that case the Microsoft.IdentityModel.JsonWebTokens library throws an exception similar to the one you describe "));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "Jwt:Issuer",
                audience: "Jwt:Audience",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
