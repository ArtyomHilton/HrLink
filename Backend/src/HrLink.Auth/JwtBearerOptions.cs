namespace HrLink.Auth;

public class JwtBearerOptions
{
    public bool ValidateAudience { get; set; } = true;
    public string Audience { get; set; } = string.Empty;
    public bool ValidateIssuer { get; set; } = true;
    public string Issuer { get; set; } = String.Empty;
    public bool ValidateLifetime { get; set; } = true;
    public bool ValidateIssuerSigningKey { get; set; } = true;
    public string SecretKey { get; set; } = string.Empty;
    public int ExpiresHours { get; set; } = default;
}