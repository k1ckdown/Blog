namespace Blog.Infrastructure.Options;

internal sealed class JwtOptions
{
    public required string Key { get; set; }
    public required string RefreshKey { get; set; }
    
    public required string Issuer { get; set; }
    public required string Audience { get; set; }
    
    public required double LifetimeInMinutes { get; set; }
    public required double RefreshLifetimeInDays { get; set; }
}