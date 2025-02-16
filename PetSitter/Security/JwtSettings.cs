namespace PetSitter.Security;


public class JwtSettings
{
    public required string Issuer { get; set; }
    public required string Audience { get; set; }
    public required string SecretKey { get; set; }
    public required int TokenExpiresInMinutes { get; set; }
    public bool RequireHttpsMetadata { get; set; } = true;
}