using HomeMaintenance.Ai.Providers.Abstractions;
using HomeMaintenance.Core.Agents.Abstractions;
using HomeMaintenance.Core.Enums;
using HomeMaintenance.Core.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OllamaSharp;
using OllamaSharp.Models;
using System.Text.Json;

namespace HomeMaintenance.Ai.Agents;

public class CreateTasksAgent : IAgent<string, string>
{
    private const string AgentName = "CreateTasksAgent";
    private const float Temperature = 0.1f;

    private readonly IPromptProvider _promptProvider;
    private readonly ILogger<CreateTasksAgent> _logger;
    private readonly OllamaApiClient _client;

    public CreateTasksAgent(
        ILogger<CreateTasksAgent> logger,
        [FromKeyedServices(PromptProvider.File)]
        IPromptProvider promptProvider)
    {
        _logger = logger;
        _promptProvider = promptProvider;

        _client = OllamaClientFactory.Create();
    }

    public async Task<string> RunAsync(
        string userText,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("• Extraindo tarefas do texto...");

        var instructions = await _promptProvider.GetPromptAsync(
            AgentName,
            cancellationToken);

        var prompt = $"""
            {instructions}

            Texto do usuário:

            {userText}
            """;

        var finalResponse = string.Empty;

        await foreach (var chunk in _client.GenerateAsync(
                           new GenerateRequest
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

        _logger.LogInformation("---");
        _logger.LogInformation(finalResponse);
        _logger.LogInformation("---");

        return finalResponse;
    }
}