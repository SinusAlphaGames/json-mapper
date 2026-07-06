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
    public async Task Handle(CreateJsonMappingCommand request, CancellationToken cancellationToken)
    {
        var flattenFirstJsonFields = JsonFlattener.Flatten(request.FirstJson);
        SaveEmbeddings(flattenFirstJsonFields, cancellationToken);
        
        var flattenSecondJsonFields = JsonFlattener.Flatten(request.SecondJson);
        var secondJsonGuid = SaveEmbeddings(flattenSecondJsonFields, cancellationToken);


        var sourceFieldCandidates = await FindCandidates(flattenFirstJsonFields, secondJsonGuid, cancellationToken);

        foreach (var source in sourceFieldCandidates)
        {
            logger.LogInformation("Source = {source} - {candidateCount}",  source.SourceField,  source.Candidates.Count);
            foreach (var candidate in source.Candidates)
            {
                logger.LogInformation("Candidate = {path} - {score}",  candidate.Payload.Path, candidate.Score);
            }
        }
        
        // var testEmbedding = embeddingProvider.GetEmbedding("id", cancellationToken);
        // embeddingRepository.SearchEmbedding(testEmbedding.Result);
        
        // return Task.FromResult(0);
    }

    private Guid SaveEmbeddings(IReadOnlyList<FieldNode> flattenJsonFields, CancellationToken cancellationToken)
    {
        var documentGuid = Guid.NewGuid();
        foreach (var flattenJsonField in flattenJsonFields)
        {
            var embedding = embeddingProvider.GetEmbedding(flattenJsonField.Path, cancellationToken);
            embeddingRepository.PutEmbedding(Guid.NewGuid(), documentGuid, flattenJsonField.Path, embedding.Result);    
        }

        return documentGuid;
    }
    
    private async Task<List<SourceFieldCandidates>> FindCandidates(IReadOnlyCollection<FieldNode> sourceFields, Guid targetDocumentId, CancellationToken cancellationToken)
    {
        var result = new List<SourceFieldCandidates>();

        foreach (var field in sourceFields)
        {
            var embedding = await embeddingProvider.GetEmbedding(field.Path, cancellationToken);
            var candidates = await embeddingRepository.SearchEmbedding(embedding, targetDocumentId);

            result.Add(new SourceFieldCandidates
            {
                SourceField = field.Path,
                Candidates = candidates.ToList()
            });
        }

        return result;
    }
}