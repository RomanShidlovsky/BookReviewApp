using Identity.API.IdentityServerConfiguration;
using Identity.DataAccess.Entities;
using Shared.Extensions;

namespace Identity.API.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureCors();
        services.AddAuthentication();
        services.AddAuthorization();
        services.AddControllers();
        services.ConfigureIdentityServer(configuration);
        services.ConfigureSwagger(configuration);
    }

    private static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
    }

    private static void ConfigureIdentityServer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentityServer(opt => 
            opt.IssuerUri = configuration["IdentityServer:IssuerUri"])
            .AddAspNetIdentity<User>()
            .AddInMemoryApiScopes(Configuration.GetApiScopes())
            .AddInMemoryApiResources(Configuration.GetApis())
            .AddInMemoryClients(Configuration.GetClients())
            .AddInMemoryIdentityResources(Configuration.GetIdentityResources())
            .AddDeveloperSigningCredential()
            .AddProfileService<ProfileService>();
    }
}