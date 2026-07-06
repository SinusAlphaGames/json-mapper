using Domain.Json;

namespace JsonMapper.Infrastructure.Embedding.QdrantIntegration.Search;

public class QdrantSearchResponse
{
    public List<EmbeddingSearchResult> Result { get; set; }
}