using System.Text.Json.Nodes;
using Domain.Json;
using MediatR;

namespace JsonMapper.Application.Json.apply;

public record ApplyJsonMappingCommand(
    JsonObject SourceJson,
    JsonObject TargetJson,
    List<FieldMapping> Mappings
) : IRequest<JsonObject>;