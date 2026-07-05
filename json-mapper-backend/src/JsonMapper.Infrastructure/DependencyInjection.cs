using JsonMapper.Application.Json;
using JsonMapper.Infrastructure.Embedding;
using JsonMapper.Infrastructure.Embedding.QdrantIntegration;
using Microsoft.Extensions.DependencyInjection;

namespace JsonMapper.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
    {
        services.AddHttpClient<IEmbeddingProvider, OllamaEmbeddingProvider>(client => client.BaseAddress = new Uri("http://localhost:11434"));
        services.AddHttpClient<IEmbeddingRepository, QdrantEmbeddingRepository>(client => client.BaseAddress = new Uri("http://localhost:6333"));
        return services;
    }
}