using Domain.Mappings;
using JsonMapper.Application.Json.get;
using JsonMapper.Application.Json.GetList;

namespace JsonMapper.Application.Json;

public interface IJsonMappingRepository
{
    public Task AddAsync(JsonMapping mapping, CancellationToken cancellationToken);
    
    public Task<List<MappingDto>> GetAllAsync(CancellationToken cancellationToken);
    
    Task<List<JsonMappingListDto>> GetMappingsAsync(CancellationToken cancellationToken);
    
}