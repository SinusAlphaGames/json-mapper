using System.Net.Http.Json;
using JsonMapper.Application.Json;
using Microsoft.Extensions.Logging;

namespace JsonMapper.Infrastructure.Embedding.QdrantIntegration;

public class QdrantEmbeddingRepository(HttpClient httpClient, ILogger<QdrantEmbeddingRepository> logger) : IEmbeddingRepository
{
    public async Task PutEmbedding(string propertyPath, float[] embedding)
    {
        var QdrantPoint = new QdrantPoint();
        QdrantPoint.Id = 1;
        QdrantPoint.Vector = embedding;
        QdrantPoint.Payload = new
        {
            path = propertyPath
        };

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
}