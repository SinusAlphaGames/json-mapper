using System.Net.Http.Json;
using JsonMapper.Application.Json;
using JsonMapper.Infrastructure.Embedding.QdrantIntegration.Search;
using Microsoft.Extensions.Logging;

namespace JsonMapper.Infrastructure.Embedding.QdrantIntegration;

public class QdrantEmbeddingRepository(HttpClient httpClient, ILogger<QdrantEmbeddingRepository> logger) : IEmbeddingRepository
{
    public async Task PutEmbedding(long id, long documentId, string propertyPath, float[] embedding)
    {
        var QdrantPoint = new QdrantPoint();
        QdrantPoint.Id = id;
        QdrantPoint.Vector = embedding;
        QdrantPoint.Payload = new QdrantPointPayload(propertyPath, documentId);

        var request = new QdrantUpsertRequest();
        request.Points.Add(QdrantPoint);
        
        await CreateCollectionIfNotExists(embedding);
        
        var response = await httpClient.PutAsJsonAsync("/collections/json_fields/points", request);
    }

    //TODO to mozna przeniesc gdzies gdzie sie wykona raz np przy starcie aplikacji, a nie co request
    private async Task CreateCollectionIfNotExists(float[] embedding)
    {
        var getCollectionResponse = await httpClient.GetAsync("/collections/json_fields");
        if (getCollectionResponse.IsSuccessStatusCode) return;
        
        var putCollectionResponse = await httpClient.PutAsJsonAsync("/collections/json_fields", new
        {
            vectors = new
            {
                size = embedding.Length,
                distance = "Cosine"
            }
        });

        putCollectionResponse.EnsureSuccessStatusCode();
    }
    
    public async Task<float> SearchEmbedding(float[] embedding)
    {
        var response = await httpClient.PostAsJsonAsync("/collections/json_fields/points/search", new
        {
            vector = embedding,
            limit = 5,
            with_payload = true
        });
        
        var result = await response.Content.ReadFromJsonAsync<QdrantSearchResponse>();
        
        foreach (var qdrantSearchedValue in result!.Result)
        {
            logger.LogInformation("Qdrant result = {qdrantSearchedValueScore}: payload = {payload}", qdrantSearchedValue.Score, qdrantSearchedValue.Payload.Path);    
        }
        return result!.Result.FirstOrDefault().Score;
    }
}