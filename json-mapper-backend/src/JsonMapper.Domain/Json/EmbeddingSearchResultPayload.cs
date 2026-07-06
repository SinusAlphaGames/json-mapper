namespace Domain.Json;

public class EmbeddingSearchResultPayload
{
    public string Path { get; init; } = null!;
    public Guid DocumentId { get; init; }
}