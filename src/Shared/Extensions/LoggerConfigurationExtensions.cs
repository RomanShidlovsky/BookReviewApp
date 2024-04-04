using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace Shared.Extensions;

public static class LoggerConfigurationExtensions
{
    public static void ConfigureLogging(
        this IServiceCollection services,
        WebApplicationBuilder builder,
        string assemblyName)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")!;
        
        builder.Host.UseSerilog((context, configuration) =>
        {
            var configurationRoot = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .Build();

            configuration
                .ReadFrom.Configuration(configurationRoot)
                .WriteTo.Elasticsearch(ConfigureElasticsearchSink(configurationRoot, environment, assemblyName))
                .Enrich.WithProperty("Environment", environment);
        });
    }
    
    private static ElasticsearchSinkOptions ConfigureElasticsearchSink(
        IConfigurationRoot configuration,
        string environment,
        string assemblyName)
    {
        return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]!))
        {
            IndexFormat = $"{assemblyName.ToLower().Replace(".", "-")}-{environment.ToLower()}-{DateTime.UtcNow:yyyy-MM}",
            AutoRegisterTemplate = true,
            TemplateName = $"{assemblyName.ToLower().Replace(".", "-")}-{environment.ToLower()}",
            AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
            TypeName = null,
            BatchAction = ElasticOpType.Create,
            NumberOfReplicas = 1,
            NumberOfShards = 2
        };
    }
}