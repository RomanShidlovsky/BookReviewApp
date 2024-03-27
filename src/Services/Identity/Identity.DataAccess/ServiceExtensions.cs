using System.Reflection;
using Identity.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.DataAccess;

public static class ServiceExtensions
{
    public static void ConfigureDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IdentityContext>(
            opt =>
                opt.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName)));
    }
}