using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Review.Domain.Interfaces.Repositories;
using Review.Infrastructure.Context;
using Review.Infrastructure.Repositories;

namespace Review.Infrastructure;

public static class ServiceExtensions
{
    public static void ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureDbContext(configuration);
        services.ConfigureRepositories();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        var databaseName = configuration["MongoDB:DatabaseName"]!;

        var client = new MongoClient(connectionString);
        services.AddSingleton<IMongoDatabase>(client.GetDatabase(databaseName));

        services.AddDbContext<ReviewContext>(options =>
            options.UseMongoDB(client, databaseName));
    }

    private static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }
}