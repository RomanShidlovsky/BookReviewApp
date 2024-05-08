using System.Reflection;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Review.Application.Behaviors;

namespace Review.Application;

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
        services.AddMassTransit(configurator =>
        {
            var assembly = Assembly.GetExecutingAssembly();
            
            configurator.AddConsumers(assembly);
            configurator.UsingRabbitMq((context, busConfigurator) =>
            {
                busConfigurator.Host("rabbitmq", "/");
                
                busConfigurator.ConfigureEndpoints(context);
            });
        });
    }
}