using HomeMaintenance.Ai.Providers.Abstractions;
using HomeMaintenance.Core.Agents.Abstractions;
using HomeMaintenance.Core.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OllamaSharp;

namespace CleaningSchedule.Ai.Agents
{
    public class HomeMaintenancePlannerAgent : IAgent<IEnumerable<string>, string>
    {
        private const string AgentName = "HomeMaintenancePlannerAgent";

        private readonly IPromptProvider _promptProvider;
        private readonly OllamaApiClient _client;
        private const float Temperature = 0.1f;
        private ILogger<HomeMaintenancePlannerAgent> _logger;

        public HomeMaintenancePlannerAgent(ILogger<HomeMaintenancePlannerAgent> logger, [FromKeyedServices(PromptProvider.File)] IPromptProvider promptProvider)
        {
            _logger = logger;
            _promptProvider = promptProvider;

            _client = OllamaClientFactory.Create();
        }

        public Task<string> RunAsync(IEnumerable<string> data, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
