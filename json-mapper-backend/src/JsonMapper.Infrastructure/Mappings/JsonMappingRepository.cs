using Domain.Mappings;
using JsonMapper.Application.Json;
using JsonMapper.Application.Json.get;
using JsonMapper.Application.Json.GetList;
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
    
    public async Task<List<JsonMappingListDto>> GetMappingsAsync(
        CancellationToken cancellationToken)
    {
        return await context.JsonMappings
            .Include(x => x.Fields)
            .Select(x => new JsonMappingListDto
            {
                Id = x.Id,

                CreatedAt = x.CreatedAt,

                Mappings = x.Fields
                    .Select(field => new MappingDto
                    {
                        SourceField = field.SourceField,
                        TargetField = field.TargetField
                    })
                    .ToList()
            })
            .ToListAsync(cancellationToken);
    }
}