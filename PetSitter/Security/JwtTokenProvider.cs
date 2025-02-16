namespace PetSitter.Security;

using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;


public sealed class JwtTokenProvider(IOptions<JwtSettings> configurations)
{
    private readonly JwtSettings _jwtSettings = configurations.Value;

    public string GenerateToken(string clientId, Dictionary<string, object> roleClaims)
    {
        // https://learn.microsoft.com/en-us/entra/identity-platform/id-token-claims-reference
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var securityTokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim("sub", clientId)
            ]),
            Claims = roleClaims,
            Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpiresInMinutes),
            SigningCredentials = signingCredentials,
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
        };
        var jsonWebTokenHandler = new JsonWebTokenHandler();
        return jsonWebTokenHandler.CreateToken(securityTokenDescriptor);
    }
}
