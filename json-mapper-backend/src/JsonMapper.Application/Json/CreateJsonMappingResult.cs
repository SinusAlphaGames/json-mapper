using Domain.Json;

namespace JsonMapper.Application.Json;

public class CreateJsonMappingResult
{
    public List<FieldMapping> Mappings { get; init; } = [];
    public List<string> UnmappedSourcePaths { get; init; } = [];
    public List<string> UnusedTargetPaths { get; init; } = [];
}