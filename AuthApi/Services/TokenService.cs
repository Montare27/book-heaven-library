namespace AuthApi.Services
{
    using Microsoft.IdentityModel.Tokens;
    using Models;
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
            var jwtSecurityToken = new JwtSecurityToken(
            header: new JwtHeader(
                    signingCredentials: new SigningCredentials(
                        algorithm: SecurityAlgorithms.HmacSha256,
                        key: new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]!))
                    )  
                ),
                payload: new JwtPayload(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    notBefore: DateTime.Now + TimeSpan.FromMinutes(15),
                    expires: DateTime.Now + TimeSpan.FromMinutes(15)
                )
            );

            var result = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return result;
        }

        public List<Claim> CreateClaims(ClaimsModel model)
        {
            var claims =  new List<Claim>(){
                new Claim(ClaimTypes.Email, model.UserName),
                new Claim(ClaimTypes.NameIdentifier, model.Id.ToString()),
            };

            foreach (var role in model.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }
    }
}
