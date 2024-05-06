using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Review.Infrastructure.Seed;

namespace Review.Infrastructure.Extensions;

public static class SeedExtensions
{
    public static void AddSeedData(this IApplicationBuilder app)
    {
        using var services = app.ApplicationServices.CreateScope();

        var seedInitializer = services.ServiceProvider.GetService<ISeedInitializer>();
        seedInitializer?.Init();
    }
}