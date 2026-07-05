namespace JsonMapper.Application.Json;

public interface IEmbeddingProvider
{
    Task<float[]> GetEmbedding(string text, CancellationToken cancellationToken);
}