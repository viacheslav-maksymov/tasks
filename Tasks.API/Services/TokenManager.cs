using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tasks.API.Constants;
using Tasks.API.Services.Interfaces;

namespace Tasks.API.Services
{
    public sealed class TokenManager : ITokenManager
    {
        private readonly IConfiguration configuration;

        public TokenManager(IConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public string GetToken(string userId, string email, IEnumerable<string> roles)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.configuration["Authentication:Secret"]));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>
            {
                new Claim(TokenClaimTypes.IdClaim, userId), 
                new Claim(ClaimTypes.Email, email),
            };

            foreach (string role in roles)
                claimsForToken.Add(new Claim(ClaimTypes.Role, role));

            var jwtSecurityToken = new JwtSecurityToken(
                this.configuration["Authentication:Issuer"],
                this.configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(2),
                signingCredentials);

            string token = new JwtSecurityTokenHandler()
                .WriteToken(jwtSecurityToken);

            return token;
        }
    }
}
