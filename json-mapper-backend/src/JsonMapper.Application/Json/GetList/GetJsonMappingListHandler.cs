using JsonMapper.Application.Json.get;
using MediatR;

namespace JsonMapper.Application.Json.GetList;

public class GetJsonMappingListHandler(IJsonMappingRepository repository) : IRequestHandler<GetJsonMappingListQuery, List<JsonMappingListDto>>
{
    public async Task<List<JsonMappingListDto>> Handle(GetJsonMappingListQuery request, CancellationToken cancellationToken)
    {
        return await repository.GetMappingsAsync(cancellationToken);
    }
}