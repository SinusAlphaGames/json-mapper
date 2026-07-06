using System.Text.Json;
using System.Text.Json.Nodes;
using MediatR;

namespace JsonMapper.Application.Json;

public record CreateJsonMappingCommand(JsonNode FirstJson, JsonNode SecondJson) : IRequest<CreateJsonMappingResult>
{
    public JsonNode FirstJson { get; set; } = FirstJson;
    public JsonNode SecondJson { get; set; } = SecondJson;
    
}