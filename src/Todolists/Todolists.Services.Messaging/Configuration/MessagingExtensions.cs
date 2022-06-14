﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using RabbitMQ.Client;
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
        services
            .AddOptions()
            .AddRabbitMq(configuration, messageHandlerType);

        return services;
    }

    private static IServiceCollection AddRabbitMq(
        this IServiceCollection services,
        IConfiguration configuration,
        Type messageHandlerType = null)
    {
        services
            .AddSingleton<IMessageService, MessageService>()
            .AddTransient<IHandlerFactory, HandlerFactory>()
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