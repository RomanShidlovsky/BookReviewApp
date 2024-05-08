using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;

namespace Ocelot.ApiGateway.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureServices(this IServiceCollection services, IConfigurationManager configuration,
        IWebHostEnvironment environment)
    {
        services.ConfigureOcelot(configuration, environment);
        services.ConfigureCors();
        services.AddAuthorization();
        services.AddControllers();
        services.ConfigureIdentityServer(configuration);
    }

    public static void ConfigureOcelot(this IServiceCollection services, IConfigurationManager configuration,
        IWebHostEnvironment environment)
    {
        configuration
            .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"ocelot.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

        services.AddOcelot(configuration);
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
        var privateKey = configuration["JwtOptions:Key"];

        var privateKeyBytes = Convert.FromBase64String(privateKey);
        var rsa = RSA.Create(2048);
        rsa.ImportRSAPrivateKey(privateKeyBytes, out _);
        var key = new RsaSecurityKey(rsa);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.Authority = configuration["IdentityServer:IssuerUri"];
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters.ValidateAudience = false;
            options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
            options.TokenValidationParameters.ValidIssuer = configuration["IdentityServer:IssuerUri"];
            options.TokenValidationParameters.IssuerSigningKey = new RsaSecurityKey(key.Rsa.ExportParameters(false));
        });
    }
}