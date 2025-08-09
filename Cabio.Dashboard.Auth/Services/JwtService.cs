using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Cabio.Dashboard.Auth
{
    public class JwtService : IJwtService
    {
        private readonly string _secret;
        private readonly string _issuer;
        private const int TokenExpiryHours = 2;

        public JwtService(string secret, string issuer)
        {
            _secret = secret;
            _issuer = issuer;
        }

        public string GenerateToken(string username, string role)
        {
            var key = Encoding.UTF8.GetBytes(_secret);
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = CreateTokenDescriptor(username, role, key);

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        
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
