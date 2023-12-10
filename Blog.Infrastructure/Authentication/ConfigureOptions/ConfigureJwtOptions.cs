using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Blog.Infrastructure.Authentication.ConfigureOptions;

internal sealed class ConfigureJwtOptions : IConfigureOptions<JwtOptions>
{
    private const string SectionName = "JWTOptions";
    private readonly IConfiguration _configuration;

    public ConfigureJwtOptions(IConfiguration configuration) =>
        _configuration = configuration;

    public void Configure(JwtOptions options)
    {
        _configuration.GetSection(SectionName).Bind(options);
    }
}