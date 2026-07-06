using Domain.Json;

namespace JsonMapper.Application.Json;

public interface IEmbeddingRepository
{
    Task PutEmbedding(Guid id, Guid documentId, string path, float[] embedding);
    
    Task<IReadOnlyList<EmbeddingSearchResult>> SearchEmbedding(float[] embedding, Guid documentId);
}