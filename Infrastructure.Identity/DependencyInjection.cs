using System.IdentityModel.Tokens.Jwt;
using Application.Common.Interfaces.Services;
using Infrastructure.Identity.Authentication;
using Infrastructure.Identity.Authentication.ConfigureOptions;
using Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Identity;

public static class DependencyInjection
{
    public static void AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

        services.ConfigureOptions<ConfigureJwtOptions>();
        services.ConfigureOptions<ConfigureJwtBearerOptions>();

        services.AddScoped<JwtProvider>();
        services.AddScoped<JwtSecurityTokenHandler>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddRedis(configuration);
    }

    private static void AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("RedisConnection");
        });
    }
}