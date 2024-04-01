using System.Security.Cryptography.X509Certificates;
using FluentValidation.AspNetCore;
using Identity.API.IdentityServerConfiguration;
using Identity.DataAccess.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Shared.Extensions;

namespace Identity.API.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureCors();
        services.ConfigureAuthentication(configuration);
        services.AddAuthorization();
        services.AddControllers();
        services.AddFluentValidationAutoValidation();
        services.ConfigureIdentityServer(configuration);
        services.ConfigureSwagger(configuration);
    }

    private static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var cert = new X509Certificate2(
            Path.Combine(Environment.CurrentDirectory, $"Certificates/{configuration["JwtOptions:CertificateName"]}"),
            configuration["JwtOptions:CertificatePassword"]);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.Authority = configuration["IdentityServer:IssuerUri"];
            options.TokenValidationParameters.ValidateAudience = false;
            options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
            options.TokenValidationParameters.IssuerSigningKey = new X509SecurityKey(cert);
        });
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
        var cert = new X509Certificate2(
            Path.Combine(Environment.CurrentDirectory, $"Certificates/{configuration["JwtOptions:CertificateName"]}"),
            configuration["JwtOptions:CertificatePassword"]);

        var jwtOptions = new JwtOptions();
        configuration.GetSection("JwtOptions").Bind(jwtOptions);

        services.AddIdentityServer(opt =>
                opt.IssuerUri = configuration["IdentityServer:IssuerUri"])
            .AddAspNetIdentity<User>()
            .AddInMemoryApiScopes(Configuration.GetApiScopes())
            .AddInMemoryApiResources(Configuration.GetApis())
            .AddInMemoryClients(Configuration.GetClients(jwtOptions))
            .AddInMemoryIdentityResources(Configuration.GetIdentityResources())
            .AddSigningCredential(cert)
            .AddProfileService<ProfileService>();
    }
}