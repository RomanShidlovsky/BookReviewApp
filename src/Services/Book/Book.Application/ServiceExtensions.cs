using System.Reflection;
using Book.Application.Behaviors;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Book.Application;

public static class ServiceExtensions
{
    public static void ConfigureApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        services.AddMessageBroker();
        
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }
    
    public static void AddMessageBroker(this IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();
            x.AddConsumers(Assembly.GetExecutingAssembly());
            
            x.UsingRabbitMq((context, busFactoryConfigurator) =>
            {
                busFactoryConfigurator.Host("rabbitmq", "/");
                busFactoryConfigurator.ConfigureEndpoints(context);
            });
        });
    }
}