using Book.Domain.Interfaces.Repositories;
using Book.Infrastructure.Context;
using Book.Infrastructure.Repositories;
using Book.Infrastructure.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Book.Infrastructure.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureDbContext(configuration);
        services.ConfigureRepositories();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddSingleton<ISeedInitializer, SeedInitializer>();
    }
    
    private static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BookContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });
    }

    private static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<ISubjectRepository, SubjectRepository>();
        services.AddScoped<ILanguageRepository, LanguageRepository>();
    }
}