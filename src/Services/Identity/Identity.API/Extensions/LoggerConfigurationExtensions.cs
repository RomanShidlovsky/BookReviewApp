using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace Identity.API.Extensions;

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
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .WriteTo.Debug()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(ConfigureElasticsearchSink(configurationRoot, environment, assemblyName))
                .Enrich.WithProperty("Environment", environment)
                .ReadFrom.Configuration(configurationRoot);
        });
    }
    
    private static ElasticsearchSinkOptions ConfigureElasticsearchSink(
        IConfigurationRoot configuration,
        string environment,
        string assemblyName)
    {
        return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]!))
        {
            AutoRegisterTemplate = true,
            AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
            IndexFormat =
                $"{assemblyName.ToLower().Replace(".", "-")}-{environment.ToLower()}-{DateTime.UtcNow:yyyy-MM}",
            NumberOfReplicas = 1,
            NumberOfShards = 2
        };
    }
}