using Domain.Mappings;
using JsonMapper.Application.Json.get;

namespace JsonMapper.Application.Json;

public interface IJsonMappingRepository
{
    public Task AddAsync(JsonMapping mapping, CancellationToken cancellationToken);
    
    public Task<List<MappingDto>> GetAllAsync(CancellationToken cancellationToken);
    
}