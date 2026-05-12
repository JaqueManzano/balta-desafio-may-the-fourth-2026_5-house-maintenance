using HomeMaintenance.Ai.Agents;
using HomeMaintenance.Ai.Providers;
using HomeMaintenance.Ai.Providers.Abstractions;
using HomeMaintenance.Core.Agents.Abstractions;
using HomeMaintenance.Core.Enums;
using HomeMaintenance.Core.Models;
using Microsoft.Extensions.DependencyInjection;

namespace HomeMaintenance.Ai;

public static class DependencyInjection
{
    public static IServiceCollection AddAgents(this IServiceCollection services)
    {
        services.AddKeyedTransient<IAgent<string, string>, HomeMaintenancePlannerAgent>(AgentType.HomeMaintenancePlannerAgent);
        services.AddKeyedTransient<IAgent<string, string>, CreateTasksAgent>(AgentType.CreateTasksAgent);
        
        services.AddKeyedTransient<IPromptProvider, FilePromptProvider>(PromptProvider.File);

        return services;
    }
}