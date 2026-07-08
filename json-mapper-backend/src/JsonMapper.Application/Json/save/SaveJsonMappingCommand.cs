using MediatR;

namespace JsonMapper.Application.Json.save;

public record SaveJsonMappingCommand(
    List<JsonFieldMappingDto> Mappings
) : IRequest;