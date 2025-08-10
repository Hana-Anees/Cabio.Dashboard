using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Cabio.Dashboard.Auth.Services
{
    public class JwtService : IJwtService
    {
        private readonly string _secret;
        private readonly string _issuer;
        private const int TokenExpiryHours = 2; // Make expiry configurable via appsettings
        private readonly byte[] _keyBytes;
        private readonly JwtSecurityTokenHandler _tokenHandler = new();

        public JwtService(string secret, string issuer)
        {
            _secret = secret;
            _issuer = issuer;
            _keyBytes = Encoding.UTF8.GetBytes(secret);
        }

        public string GenerateToken(string username, string role) =>
            _tokenHandler.WriteToken(
                _tokenHandler.CreateToken(
                    CreateTokenDescriptor(username, role, _keyBytes)
                )
            );

        private SecurityTokenDescriptor CreateTokenDescriptor(string username, string role, byte[] key)
        {
            return new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(GetClaims(username, role)),
                Expires = DateTime.UtcNow.AddHours(TokenExpiryHours),
                Issuer = _issuer,
                Audience = _issuer,
                SigningCredentials = GetSigningCredentials(key)
            };
        }
        
        private static IEnumerable<Claim> GetClaims(string username, string role) =>
            new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            };
       
        private static SigningCredentials GetSigningCredentials(byte[] key) =>
            new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
    }
}
