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

        var id = 0;
        foreach (var flattenJsonField in flattenJsonFields)
        {
            id++;
            var embedding = embeddingProvider.GetEmbedding(flattenJsonField.Path, cancellationToken);
            embeddingRepository.PutEmbedding(id, flattenJsonField.Path, embedding.Result);    
        }
        
        
        var testEmbedding = embeddingProvider.GetEmbedding("id", cancellationToken);
        embeddingRepository.SearchEmbedding(testEmbedding.Result);
        
        return Task.FromResult(0);
    }
}