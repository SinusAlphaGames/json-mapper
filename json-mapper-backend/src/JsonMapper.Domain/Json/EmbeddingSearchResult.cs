namespace Domain.Json;

public class EmbeddingSearchResult
{
    public Guid Id { get; set; }
    public EmbeddingSearchResultPayload Payload { get; init; } = null!;
    public double Score { get; init; }
}