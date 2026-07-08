using Domain.Mappings;
using JsonMapper.Application.Json;
using JsonMapper.Application.Json.get;
using JsonMapper.Application.Json.save;
using JsonMapper.Infrastructure.PostgresDbConfiguration;
using Microsoft.EntityFrameworkCore;

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
    
    public async Task<List<MappingDto>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await context.JsonFieldMappings
            .Select(x => new MappingDto
            {
                SourceField = x.SourceField,
                TargetField = x.TargetField
            })
            .ToListAsync(cancellationToken);
    }
}