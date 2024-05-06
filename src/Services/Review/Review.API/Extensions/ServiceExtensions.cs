using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Review.Infrastructure.Context;
using Shared.Extensions;

namespace Review.API.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureCors();
        services.AddAuthorization();
        services.AddControllers();
        services.ConfigureIdentityServer(configuration);
        services.ConfigureSwagger(configuration);
        services.Configure<ReviewDatabaseSettings>(
            configuration.GetSection(nameof(ReviewDatabaseSettings)));
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