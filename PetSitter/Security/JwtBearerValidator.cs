namespace PetSitter.Security;

using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

public class JwtBearerValidator(IConfigurationSection configuration)
{
    private readonly JwtSettings _jwtSettings = configuration.Get<JwtSettings>()!;

    public void Configure(JwtBearerOptions jwtOptions)
    {
        jwtOptions.RequireHttpsMetadata = _jwtSettings.RequireHttpsMetadata;
        jwtOptions.Audience = _jwtSettings.Audience;
        jwtOptions.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey)),
            ValidIssuer = _jwtSettings.Issuer,
            ValidAudience = _jwtSettings.Audience,
            ClockSkew = TimeSpan.Zero,
        };
    }
}