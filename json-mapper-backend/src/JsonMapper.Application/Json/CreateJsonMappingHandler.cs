using System.Text.Json.Nodes;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JsonMapper.Application.Json;

public class CreateJsonMappingHandler(
            ILogger<CreateJsonMappingHandler> logger,
            IEmbeddingProvider embeddingProvider,
            IEmbeddingRepository embeddingRepository
    ) : IRequestHandler<CreateJsonMappingCommand>
{
    public Task Handle(CreateJsonMappingCommand request, CancellationToken cancellationToken)
    {
        var jsonNode = JsonNode.Parse(request.Json);
        var flattenJsonFields = JsonFlattener.Flatten(jsonNode);
        logger.LogInformation("Flatten json field path = {Path}",  flattenJsonFields[0].Path);
        var embedding = embeddingProvider.GetEmbedding(flattenJsonFields[0].Path, cancellationToken);
        
        embeddingRepository.PutEmbedding(flattenJsonFields[0].Path, embedding.Result);
        
        return Task.FromResult(0);
    }
}