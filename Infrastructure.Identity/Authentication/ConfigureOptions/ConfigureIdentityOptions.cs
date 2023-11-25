using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Infrastructure.Identity.Authentication.ConfigureOptions;

internal sealed class ConfigureIdentityOptions : IConfigureNamedOptions<IdentityOptions>
{
    public void Configure(IdentityOptions options)
    {
        options.User.RequireUniqueEmail = true;
        
        options.Password.RequiredLength = 6;
        options.Password.RequireDigit = true;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
    }

    public void Configure(string? name, IdentityOptions options)
    {
        Configure(options);
    }
}