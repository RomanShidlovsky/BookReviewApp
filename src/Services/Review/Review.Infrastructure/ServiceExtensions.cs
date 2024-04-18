using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Review.Domain.Interfaces.Repositories;
using Review.Infrastructure.Context;
using Review.Infrastructure.Interfaces;
using Review.Infrastructure.Repositories;

namespace Review.Infrastructure;

public static class ServiceExtensions
{
    public static void ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureDbContext(configuration);
        services.AddScoped<IUnitOfWork, UnitOfWork>();
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