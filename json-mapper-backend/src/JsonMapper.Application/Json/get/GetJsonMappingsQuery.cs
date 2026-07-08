using MediatR;

namespace JsonMapper.Application.Json.get;

public record GetJsonMappingsQuery() : IRequest<List<MappingDto>>;