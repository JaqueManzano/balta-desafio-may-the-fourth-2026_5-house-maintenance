using HomeMaintenance.Core.Agents.Abstractions;
using HomeMaintenance.Core.Enums;
using HomeMaintenance.Core.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HomeMaintenance.Infra.Services;

public class HomeTaskService(
    ILogger<HomeTaskService> logger,

    [FromKeyedServices(AgentType.CreateTasksAgent)]
    IAgent<string, string> createTaskAgent,

    [FromKeyedServices(AgentType.HomeMaintenancePlannerAgent)]
    IAgent<string, string> plannerAgent
) : IHomeTaskService
{
    public async Task<string> SendAsync(string text, CancellationToken cancellationToken)
    {
        logger.LogInformation("• Extraindo tarefas do texto...");

        var tasks = await createTaskAgent.RunAsync(
            text,
            cancellationToken);

        logger.LogInformation("• Organizando tarefas...");

        var result = await plannerAgent.RunAsync(
            tasks,
            cancellationToken);

        return result;
    }
}