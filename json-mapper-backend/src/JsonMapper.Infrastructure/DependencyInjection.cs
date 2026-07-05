using JsonMapper.Application.Json;
using JsonMapper.Infrastructure.Embedding;
using Microsoft.Extensions.DependencyInjection;

namespace JsonMapper.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
    {
        services.AddHttpClient<IEmbeddingProvider, OllamaEmbeddingProvider>(client => client.BaseAddress = new Uri("http://localhost:11434"));
        return services;
    }
}