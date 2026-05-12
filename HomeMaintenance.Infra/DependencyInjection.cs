using HomeMaintenance.Infra.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HomeMaintenance.Infra;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {

        services.AddScoped<IHomeTaskService, HomeTaskService>();
        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {

        return services;
    }
}