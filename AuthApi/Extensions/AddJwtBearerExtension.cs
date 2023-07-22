namespace AuthApi.Extensions
{
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.IdentityModel.Tokens;
    using System.Text;

    public static class AddJwtBearerExtension
    {
        public static AuthenticationBuilder AddJwtAuthentication(this AuthenticationBuilder builder, string issuer, string secret, string[] audiences)
        {
            builder.AddJwtBearer(jwtOptions => {
                jwtOptions.RequireHttpsMetadata = false;
                jwtOptions.SaveToken = true;
                jwtOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudiences = audiences,
                    IssuerSigningKey = new SymmetricSecurityKey
                        (Encoding.UTF8.GetBytes(secret)),
                };
            });
            
            return builder;
        }
    }
}
