using System.Text.Json.Nodes;
using MediatR;

namespace JsonMapper.Application.Json.apply;

public class ApplyJsonMappingHandler 
    : IRequestHandler<ApplyJsonMappingCommand, JsonObject>
{
    public Task<JsonObject> Handle(
        ApplyJsonMappingCommand request,
        CancellationToken cancellationToken)
    {
        var result = request.TargetJson.DeepClone()
            .AsObject();

        foreach (var mapping in request.Mappings)
        {
            if (string.IsNullOrWhiteSpace(mapping.TargetField))
                continue;
            
            var value = GetValue(
                request.SourceJson,
                mapping.SourceField);

            SetValue(
                result,
                mapping.TargetField,
                value);
        }

        return Task.FromResult(result);
    }


    private static JsonNode? GetValue(JsonObject json, string path)
    {
        var parts = path.Split('.');

        JsonNode? current = json;

        foreach (var part in parts)
        {
            current = current?[part];
        }

        return current;
    }


    private static void SetValue(JsonObject json, string path, JsonNode? value)
    {
        var parts = path.Split('.');

        JsonObject current = json;

        for (int i = 0; i < parts.Length - 1; i++)
        {
            if (current[parts[i]] is not JsonObject next)
            {
                next = new JsonObject();
                current[parts[i]] = next;
            }

            current = next;
        }

        current[parts[^1]] = value?.DeepClone();
    }
}