using System.Reflection;
using FluentValidation;
using Identity.BusinessLogic.Services.Implementations;
using Identity.BusinessLogic.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.BusinessLogic;

public static class ServiceExtensions
{
    public static void ConfigureBusinessLogic(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.ConfigureServices();
    }

    private static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<ITokenService, TokenService>();
    }
}