using System.Text.Json.Nodes;
using Domain.Json;
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
        var flattenFirstJsonFields = JsonFlattener.Flatten(request.FirstJson);
        SaveEmbeddings(flattenFirstJsonFields, cancellationToken);
        
        var flattenSecondJsonFields = JsonFlattener.Flatten(request.SecondJson);
        SaveEmbeddings(flattenSecondJsonFields, cancellationToken);
        
        var testEmbedding = embeddingProvider.GetEmbedding("id", cancellationToken);
        embeddingRepository.SearchEmbedding(testEmbedding.Result);
        
        return Task.FromResult(0);
    }

    private void SaveEmbeddings(IReadOnlyList<FieldNode> flattenJsonFields, CancellationToken cancellationToken)
    {
        var documentGuid = Guid.NewGuid();
        foreach (var flattenJsonField in flattenJsonFields)
        {
            var embedding = embeddingProvider.GetEmbedding(flattenJsonField.Path, cancellationToken);
            embeddingRepository.PutEmbedding(Guid.NewGuid(), documentGuid, flattenJsonField.Path, embedding.Result);    
        }
    }
}