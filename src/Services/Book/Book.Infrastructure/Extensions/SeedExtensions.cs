using Book.Infrastructure.Seed;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Book.Infrastructure.Extensions;

public static class SeedExtensions
{
    public static void AddSeedData(this IApplicationBuilder app)
    {
        using var services = app.ApplicationServices.CreateScope();

        var seedInitializer = services.ServiceProvider.GetService<ISeedInitializer>();
        seedInitializer?.Init();
    }
}