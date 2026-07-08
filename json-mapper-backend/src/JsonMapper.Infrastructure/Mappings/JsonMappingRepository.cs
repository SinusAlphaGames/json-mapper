using Domain.Mappings;
using JsonMapper.Application.Json.save;
using JsonMapper.Infrastructure.PostgresDbConfiguration;

namespace JsonMapper.Infrastructure.Mappings;

public class JsonMappingRepository(AppDbContext context) : IJsonMappingRepository
{
    public async Task AddAsync(
        JsonMapping mapping,
        CancellationToken cancellationToken)
    {
        await context.JsonMappings.AddAsync(
            mapping,
            cancellationToken);

        await context.SaveChangesAsync(
            cancellationToken);
    }
}