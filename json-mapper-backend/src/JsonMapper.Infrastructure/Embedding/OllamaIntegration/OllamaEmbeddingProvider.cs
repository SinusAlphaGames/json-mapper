using System.Net.Http.Json;
using JsonMapper.Application.Json;
using Microsoft.Extensions.Logging;

namespace JsonMapper.Infrastructure.Embedding;

public class OllamaEmbeddingProvider(HttpClient httpClient, ILogger<OllamaEmbeddingProvider> logger) : IEmbeddingProvider
{
    public async Task<float[]> GetEmbedding(string text, CancellationToken cancellationToken)
    {
        var body = new OllamaEmbeddingRequest("nomic-embed-text", text);
        var response = await httpClient.PostAsJsonAsync("/api/embeddings", body, cancellationToken: cancellationToken);
        var result = await response.Content.ReadFromJsonAsync<OllamaEmbeddingResponse>(cancellationToken: cancellationToken);
        logger.LogInformation("Embedding: {Embedding}", result.Embedding);
        return result!.Embedding;
    }
}