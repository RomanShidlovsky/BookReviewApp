using System.Security.Cryptography;
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
        services.AddAuthorization();
        services.AddControllers();
        services.AddFluentValidationAutoValidation();
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
        /*var cert = new X509Certificate2(
            Path.Combine(Environment.CurrentDirectory, $"Certificates/{configuration["JwtOptions:CertificateName"]}"),
            configuration["JwtOptions:CertificatePassword"]);*/
        var jwtOptions = new JwtOptions();
        configuration.GetSection("JwtOptions").Bind(jwtOptions);
        
        var privateKeyBytes = Convert.FromBase64String(jwtOptions.PrivateKey);
        var publicKeyBytes = Convert.FromBase64String(jwtOptions.PublicKey);
        var rsa = RSA.Create(2048);
        rsa.ImportRSAPublicKey(publicKeyBytes, out _);
        rsa.ImportRSAPrivateKey(privateKeyBytes, out _);
        var key = new RsaSecurityKey(rsa);

        var creds = new SigningCredentials(key, SecurityAlgorithms.RsaSha256);
        
        services.AddIdentityServer(opt =>
                opt.IssuerUri = configuration["IdentityServer:IssuerUri"])
            .AddAspNetIdentity<User>()
            .AddInMemoryApiScopes(Configuration.GetApiScopes())
            .AddInMemoryApiResources(Configuration.GetApis())
            .AddInMemoryClients(Configuration.GetClients(jwtOptions))
            .AddInMemoryIdentityResources(Configuration.GetIdentityResources())
            .AddSigningCredential(creds)
            .AddProfileService<ProfileService>();
        
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