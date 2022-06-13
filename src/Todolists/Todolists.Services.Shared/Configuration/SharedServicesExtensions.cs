using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Todolists.Services.Shared.Implementation;
using Todolists.Services.Shared.Interfaces;

namespace Todolists.Services.Shared.Configuration;

public static class SharedServicesExtensions
{
    public static IServiceCollection AddSharedServices(
        this IServiceCollection services,
        Assembly mappingAssembly = null)
    {
        services
            .AddSingleton<IEncryptionService, EncryptionService>()
            .AddSingleton<IDateTimeService, DateTimeService>();

        if (mappingAssembly is not null)
        {
            services
                .AddTransient<ITranslator, Translator>()
                .AddAutoMapper(mappingAssembly);
        }

        return services;
    }
}