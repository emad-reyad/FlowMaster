using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WorkFlowEngine.Infrastructure.Abstraction;
using WorkFlowEngine.Shared.Settings;

namespace WorkFlowEngine.Infrastructure.Authentication
{
    internal class JwtService : IJwtService
    {
        private readonly JwtSettings _jwtSettings;

        public JwtService(IOptions<JwtSettings> option)
        {
            _jwtSettings = option.Value;
        }
        public string GenerateToken(Guid userId, string clientName)
        {
            var signCrededntials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)), SecurityAlgorithms.HmacSha256);
            var expireyDate = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes);
            var claim = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,userId.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName,clientName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp,expireyDate.ToString()),
            };
            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: clientName,
                claims: claim,
                expires: expireyDate,
                signingCredentials: signCrededntials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool ValidateToken(string token)
        {
            return false;
        }
    }
}
