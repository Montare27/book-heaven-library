namespace AuthApi
{
    using Models;
    using System.Security.Claims;
    using ViewModels;

    public interface ITokenService
    {
        public string GenerateToken(List<Claim> claims);
        
        public List<Claim> CreateClaims(ClaimsModel model);
    }
}
