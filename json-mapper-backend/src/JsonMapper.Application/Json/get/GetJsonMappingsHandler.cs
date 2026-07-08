using JsonMapper.Application.Json.save;
using MediatR;

namespace JsonMapper.Application.Json.get;

public class GetJsonMappingsHandler(
    IJsonMappingRepository repository)
    : IRequestHandler<GetJsonMappingsQuery, List<MappingDto>>
{
    public async Task<List<MappingDto>> Handle(
        GetJsonMappingsQuery request,
        CancellationToken cancellationToken)
    {
        return await repository.GetAllAsync(
            cancellationToken);
    }
}