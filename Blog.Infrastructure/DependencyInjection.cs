using System.IdentityModel.Tokens.Jwt;
using Blog.Application.Common.Interfaces.Services;
using Blog.Infrastructure.Authentication;
using Blog.Infrastructure.Authentication.ConfigureOptions;
using Blog.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Infrastructure;

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