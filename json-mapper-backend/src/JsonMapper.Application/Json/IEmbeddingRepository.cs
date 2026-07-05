namespace JsonMapper.Application.Json;

public interface IEmbeddingRepository
{
    Task PutEmbedding(long id, string path, float[] embedding);
    
    Task<float> SearchEmbedding(float[] embedding);
}