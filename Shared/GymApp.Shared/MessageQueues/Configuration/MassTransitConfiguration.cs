using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace GymApp.Shared.MessageQueues.Configuration;

public static class MassTransitConfiguration
{
    public static IServiceCollection AddMassTransitConfiguration(
        this IServiceCollection services,
        string rabbitMqHost,
        string rabbitMqUsername,
        string rabbitMqPassword,
        params Type[] consumerTypes)
    {
        services.AddMassTransit(x =>
        {
            foreach (var consumerType in consumerTypes)
            {
                x.AddConsumer(consumerType);
            }

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(rabbitMqHost, "/", h =>
                {
                    h.Username(rabbitMqUsername);
                    h.Password(rabbitMqPassword);
                });

                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}