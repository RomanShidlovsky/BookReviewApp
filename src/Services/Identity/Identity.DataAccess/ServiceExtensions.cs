using System.Reflection;
using Identity.DataAccess.Contexts;
using Identity.DataAccess.Entities;
using Identity.DataAccess.Seed;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.DataAccess;

public static class ServiceExtensions
{
    public static void ConfigureDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureDbContext(configuration);
        services.ConfigureIdentity();
        services.AddScoped<SeedInitializer>();
    }

    private static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IdentityContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
    }

    private static void ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, Role>(o =>
            {
                o.Password.RequiredLength = 6;
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<IdentityContext>()
            .AddDefaultTokenProviders();
    }
}