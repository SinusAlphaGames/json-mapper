using Domain.Mappings;

namespace JsonMapper.Application.Json.save;

public interface IJsonMappingRepository
{
    public Task AddAsync(JsonMapping mapping, CancellationToken cancellationToken);
}