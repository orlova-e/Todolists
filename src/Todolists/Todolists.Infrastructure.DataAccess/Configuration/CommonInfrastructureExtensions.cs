using Microsoft.Extensions.DependencyInjection;
using Todolists.Infrastructure.DataAccess.Implementation;

namespace Todolists.Infrastructure.DataAccess.Configuration;

public static class CommonInfrastructureExtensions
{
    public static IServiceCollection AddCommonInfrastructure(this IServiceCollection services)
    {
        services
            .AddScoped<IRepository, Repository>();

        return services;
    }
}