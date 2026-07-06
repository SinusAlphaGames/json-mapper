using System.Text.Json.Nodes;
using Domain.Json;
using MediatR;
using Microsoft.Extensions.Logging;


namespace JsonMapper.Application.Json;

public class CreateJsonMappingHandler(
            ILogger<CreateJsonMappingHandler> logger,
            IEmbeddingProvider embeddingProvider,
            IEmbeddingRepository embeddingRepository
    ) : IRequestHandler<CreateJsonMappingCommand, CreateJsonMappingResult>
{
    public async Task<CreateJsonMappingResult> Handle(CreateJsonMappingCommand request, CancellationToken cancellationToken)
    {
        var flattenFirstJsonFields = JsonFlattener.Flatten(request.FirstJson);
        SaveEmbeddings(flattenFirstJsonFields, cancellationToken);
        
        var flattenSecondJsonFields = JsonFlattener.Flatten(request.SecondJson);
        var secondJsonGuid = SaveEmbeddings(flattenSecondJsonFields, cancellationToken);
        
        var sourceFieldCandidates = await FindCandidates(flattenFirstJsonFields, secondJsonGuid, cancellationToken);
        var result = AssignFields(sourceFieldCandidates);
        return result;
    }

    private static CreateJsonMappingResult AssignFields(List<SourceFieldCandidates> sourceFieldCandidates)
    {
        var sources = sourceFieldCandidates
            .Select(x => x.SourceField)
            .ToList();

        var targets = sourceFieldCandidates
            .SelectMany(x => x.Candidates)
            .Select(x => x.Payload.Path)
            .Distinct()
            .ToList();
        
        var scoreMap = sourceFieldCandidates
            .SelectMany(s => s.Candidates.Select(c => new
            {
                Source = s.SourceField,
                Target = c.Payload.Path,
                c.Score
            }))
            .ToDictionary(x => (x.Source, x.Target), x => x.Score);
        
        var n = Math.Max(sources.Count, targets.Count);
        var matrix = new int[n, n];
        const int scale = 1000;
        
        for (int i = 0; i < sources.Count; i++)
        {
            for (int j = 0; j < targets.Count; j++)
            {
                if (scoreMap.TryGetValue((sources[i], targets[j]), out var score))
                    matrix[i, j] = (int)Math.Round((1.0 - score) * scale);   // cost
                else
                    matrix[i, j] = scale;          // brak dopasowania
            }
        }

        var assignments = HungarianAlgorithm.HungarianAlgorithm.FindAssignments(matrix);

        var result = new List<FieldMapping>();
        var unmapped = new List<string>();
        var usedTargets = new HashSet<int>();


        for (int i = 0; i < assignments.Length; i++)
        {
            var j = assignments[i];

            if (i >= sources.Count || j >= targets.Count)
                continue;
            
            double score = 0;

            if (scoreMap.TryGetValue((sources[i], targets[j]), out var s))
                score = s;

            if (score < 0.7)
            {
                unmapped.Add(sources[i]);
                continue;
            }
            
            result.Add(new FieldMapping
            {
                SourceField = sources[i],
                TargetField = targets[j],
                Score = score
            });
            
            usedTargets.Add(j);
        }
        
        var unusedTargets = targets
            .Select((t, i) => (t, i))
            .Where(x => !usedTargets.Contains(x.i))
            .Select(x => x.t)
            .ToList();
        
        return new CreateJsonMappingResult
        {
            Mappings = result,
            UnmappedSourcePaths = unmapped,
            UnusedTargetPaths = unusedTargets
        };
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