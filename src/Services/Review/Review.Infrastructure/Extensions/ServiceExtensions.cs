using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Review.Infrastructure.Context;
using Review.Infrastructure.Interfaces;
using Review.Infrastructure.Repositories;
using Review.Infrastructure.Seed;

namespace Review.Infrastructure.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureDbContext(configuration);
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddTransient<ISeedInitializer, SeedInitializer>();
    }

    private static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["ReviewDatabaseSettings:ConnectionString"]!;
        
        services.Configure<ReviewDatabaseSettings>(
            configuration.GetSection(nameof(ReviewDatabaseSettings)));

        services.AddSingleton<IMongoClient>(_ => new MongoClient(connectionString));

        services.AddScoped<IMongoDbContext, ReviewContext>();
    }
}