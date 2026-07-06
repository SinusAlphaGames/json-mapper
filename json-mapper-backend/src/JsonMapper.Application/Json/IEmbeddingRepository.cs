namespace JsonMapper.Application.Json;

public interface IEmbeddingRepository
{
    Task PutEmbedding(Guid id, Guid documentId, string path, float[] embedding);
    
    Task<float> SearchEmbedding(float[] embedding);
}