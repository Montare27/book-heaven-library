namespace AuthApi.Services
{
    using IdentityServer4.Validation;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using ViewModels;

    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(List<Claim> claims)
        {

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials =  new SigningCredentials
                    (new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]!)),
                SecurityAlgorithms.HmacSha256),
                Expires = DateTime.Now + TimeSpan.FromMinutes(15)
            };
            
            var jwt = new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return token;
        }

        public List<Claim> CreateClaims(ClaimsModel model)
        {
            var claims =  new List<Claim>(){
                new Claim(JwtRegisteredClaimNames.Sub, model.UserName),
                new Claim(JwtRegisteredClaimNames.Email, model.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Id", model.Id.ToString()),
            };

            foreach (var role in model.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }
    }
}
