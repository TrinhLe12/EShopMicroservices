using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BuildingBlocks.Messaging.MassTransit
{
    public static class Extentions
    {
        private static readonly string _defaultHost = "amqp://localhost:5672";
        private static readonly string _defaultUsername = "guest";
        private static readonly string _defaultPassword = "guest";

        public static IServiceCollection AddMessageBroker
            (this IServiceCollection services, IConfiguration configuration, Assembly? assembly = null)
        {
            services.AddMassTransit(config =>
            {
                config.SetKebabCaseEndpointNameFormatter();

                //Need for subscriber only to register consummer of messages
                if (assembly != null)
                    config.AddConsumers(assembly);

                //Register message broker server to be used, Masstransit will interact with this server
                config.UsingRabbitMq((context, configurator) =>
                {
                    configurator.Host(new Uri(configuration["MessageBroker:Host"] ?? _defaultHost), host =>
                    {
                        host.Username(configuration["MessageBroker:UserName"] ?? _defaultUsername);
                        host.Password(configuration["MessageBroker:Password"] ?? _defaultPassword);
                    });
                    configurator.ConfigureEndpoints(context);
                });
            });

            return services;
        }
    }
}
