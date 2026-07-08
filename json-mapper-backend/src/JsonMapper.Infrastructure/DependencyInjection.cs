using JsonMapper.Application.Json;
using JsonMapper.Application.Json.save;
using JsonMapper.Infrastructure.Embedding;
using JsonMapper.Infrastructure.Embedding.QdrantIntegration;
using JsonMapper.Infrastructure.Mappings;
using JsonMapper.Infrastructure.PostgresDbConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JsonMapper.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services,  IConfiguration configuration)
    {
        services.AddHttpClient<IEmbeddingProvider, OllamaEmbeddingProvider>(client => client.BaseAddress = new Uri("http://localhost:11434"));
        services.AddHttpClient<IEmbeddingRepository, QdrantEmbeddingRepository>(client => client.BaseAddress = new Uri("http://localhost:6333"));

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(
                configuration.GetConnectionString(
                    "DefaultConnection"));
        });
        
        services.AddScoped<IJsonMappingRepository, JsonMappingRepository>();
        return services;
    }
}