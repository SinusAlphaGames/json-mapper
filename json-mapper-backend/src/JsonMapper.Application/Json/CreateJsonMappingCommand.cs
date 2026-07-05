using MediatR;

namespace JsonMapper.Application.Json;

public record CreateJsonMappingCommand(string Json) : IRequest
{
    public string Json { get; set; } = Json;
}