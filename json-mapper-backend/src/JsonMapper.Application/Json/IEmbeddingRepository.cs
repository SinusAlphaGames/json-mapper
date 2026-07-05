namespace JsonMapper.Application.Json;

public interface IEmbeddingRepository
{
    Task PutEmbedding(string path, float[] embedding);
}