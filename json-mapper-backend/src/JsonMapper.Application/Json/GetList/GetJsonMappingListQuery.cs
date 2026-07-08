using MediatR;

namespace JsonMapper.Application.Json.GetList;

public record GetJsonMappingListQuery() : IRequest<List<JsonMappingListDto>>;