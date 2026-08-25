using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIGastroLink.Services {
    public class TokenJwtService {
        private readonly IConfiguration _config;

        public TokenJwtService(IConfiguration config) {
            _config = config;
        }

        public string GeraToken(string idUsuario, string loginUsuario, IEnumerable<string>? roles = null) {
            var jwtSettings = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim> {
                new(ClaimTypes.NameIdentifier, idUsuario),
                new(ClaimTypes.Name, loginUsuario),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            if(roles != null) {
                claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));
            }

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpireMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
