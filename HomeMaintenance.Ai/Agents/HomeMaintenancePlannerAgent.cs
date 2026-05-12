using HomeMaintenance.Ai.Providers.Abstractions;
using HomeMaintenance.Core.Agents.Abstractions;
using HomeMaintenance.Core.Enums;
using HomeMaintenance.Core.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OllamaSharp;
using OllamaSharp.Models;

namespace HomeMaintenance.Ai.Agents
{
    public class HomeMaintenancePlannerAgent : IAgent<string, string>
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

        public async Task<string> RunAsync(string data, CancellationToken cancellationToken)
        {
            _logger.LogInformation("• Organizando tarefas ...");

            var instructions = await _promptProvider.GetPromptAsync(AgentName, cancellationToken);

            var prompt = $"""
                {instructions}
                
                Lista de tarefas:
                
                {data}
                """;
            var finalResponse = string.Empty;

            await foreach (var chunk in _client.GenerateAsync(
                               new GenerateRequest()
                               {
                                   Prompt = prompt,
                                   Options = new RequestOptions
                                   {
                                       Temperature = Temperature
                                   }
                               },
                               cancellationToken))
            {
                if (!string.IsNullOrWhiteSpace(chunk?.Response))
                    finalResponse += chunk.Response;
            }

            _logger.LogInformation("• Gerado sugestão de organização das atividades ...");
            _logger.LogInformation("---");
            _logger.LogInformation(finalResponse);
            _logger.LogInformation("---");

            return finalResponse;
        }
    }
}
