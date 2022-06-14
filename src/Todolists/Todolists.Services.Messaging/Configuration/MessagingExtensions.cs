using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using RabbitMQ.Client;
using Todolists.Services.Messaging.Configuration.Models;
using Todolists.Services.Messaging.Implementation;
using Todolists.Services.Messaging.Interfaces;

namespace Todolists.Services.Messaging.Configuration;

public static class MessagingExtensions
{
    public static IServiceCollection AddMessagingServices(
        this IServiceCollection services,
        IConfiguration configuration,
        Type messageHandlerType = null)
    {
        return services
            .AddOptions()
            .AddRabbitMq(configuration, messageHandlerType);
    }

    private static IServiceCollection AddRabbitMq(
        this IServiceCollection services,
        IConfiguration configuration,
        Type messageHandlerType = null)
    {
        services
            .Configure<MessageBus>(configuration.GetSection(nameof(MessageBus)))
            .AddSingleton<IMessageService, MessageService>()
            .AddTransient<IHandlerFactory, HandlerFactory>()
            .AddSingleton<IBusInitializer, BusInitializer>()
            .AddSingleton<MessageBusParameters>()
            .AddSingleton<IConnection>(_ =>
            {
                var connectionString = configuration.GetConnectionString("RabbitMQConnection");

                var connectionFactory = new ConnectionFactory
                {
                    Uri = new Uri(connectionString),
                    DispatchConsumersAsync = true,
                    AutomaticRecoveryEnabled = true
                };

                var maxRetryAttempts = 20;
                var pausesBetweenFailures = TimeSpan.FromSeconds(5);

                var retryPolicy = Policy
                    .Handle<Exception>()
                    .WaitAndRetry(maxRetryAttempts,
                        i => pausesBetweenFailures);

                var connectionResult = retryPolicy.ExecuteAndCapture(() => connectionFactory.CreateConnection());
                return connectionResult.Result;
            });

        if (messageHandlerType != null)
        {
            services
                .Scan(scan => scan.FromAssembliesOf(messageHandlerType)
                    .AddClasses(classes => classes.AssignableTo(typeof(IMessageHandler<>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());
        }

        return services;
    }
}