namespace Domain.Json;

public class SourceFieldCandidates
{
    public string SourceField { get; init; } = null!;
    public List<EmbeddingSearchResult> Candidates { get; init; } = [];
}
