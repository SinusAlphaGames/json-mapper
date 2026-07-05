using System.Text.Json.Nodes;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JsonMapper.Application.Json;

public class CreateJsonMappingHandler(IEmbeddingProvider embeddingProvider, ILogger<CreateJsonMappingHandler> logger) : IRequestHandler<CreateJsonMappingCommand>
{
    public Task Handle(CreateJsonMappingCommand request, CancellationToken cancellationToken)
    {
        var jsonNode = JsonNode.Parse(request.Json);
        var flattenJsonFields = JsonFlattener.Flatten(jsonNode);
        logger.LogInformation("Flatten json field path = {Path}",  flattenJsonFields[0].Path);
        var embedding = embeddingProvider.GetEmbedding(flattenJsonFields[0].Path, cancellationToken);
        
        return Task.FromResult(0);
    }
}