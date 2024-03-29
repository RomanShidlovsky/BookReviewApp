namespace Identity.API.IdentityServerConfiguration;

public class JwtOptions
{
    public int AccessTokenLifetime { get; set; }
    public int RefreshTokenLifetime { get; set; }
    public int IdentityTokenLifetime { get; set; }
}