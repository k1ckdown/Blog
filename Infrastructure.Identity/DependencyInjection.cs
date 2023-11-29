using Application.Common.Interfaces.Services;
using Infrastructure.Identity.Authentication;
using Infrastructure.Identity.Authentication.ConfigureOptions;
using Infrastructure.Identity.Contexts;
using Infrastructure.Identity.Models;
using Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Identity;

public static class DependencyInjection
{
    public static void AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthDbContext(configuration);

        services.AddIdentity<ApplicationUser, ApplicationRole>().AddEntityFrameworkStores<AuthDbContext>();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

        services.ConfigureOptions<ConfigureIdentityOptions>();
        services.ConfigureOptions<ConfigureJwtOptions>();
        services.ConfigureOptions<ConfigureJwtBearerOptions>();

        services.AddScoped<JwtProvider>();
        services.AddScoped<IAccountService, AccountService>();
    }

    private static void AddAuthDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("IdentityConnection");
        services.AddDbContext<AuthDbContext>(options => options.UseNpgsql(connectionString));
    }
}