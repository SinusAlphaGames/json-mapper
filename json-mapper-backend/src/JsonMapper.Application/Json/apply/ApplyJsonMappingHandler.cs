using MediatR;
using System.Text.Json.Nodes;
using JsonMapper.Application.Json.apply;

namespace JsonMapper.Application.Json.Apply;

public class ApplyJsonMappingHandler 
    : IRequestHandler<ApplyJsonMappingCommand, JsonObject>
{
    public Task<JsonObject> Handle(
        ApplyJsonMappingCommand request,
        CancellationToken cancellationToken)
    {
        var result = request.TargetJson
            .DeepClone()
            .AsObject();

        foreach (var mapping in request.Mappings)
        {
            if (string.IsNullOrWhiteSpace(mapping.TargetField))
                continue;

            var values = GetValues(
                request.SourceJson,
                mapping.SourceField);

            SetValues(
                result,
                mapping.TargetField,
                values);
        }

        return Task.FromResult(result);
    }


    private static List<JsonNode?> GetValues(
        JsonNode node,
        string path)
    {
        var parts = path.Split('.');

        var result = new List<JsonNode?>();

        CollectValues(
            node,
            parts,
            0,
            result);

        return result;
    }


    private static void CollectValues(
        JsonNode? current,
        string[] parts,
        int index,
        List<JsonNode?> result)
    {
        if (current == null)
            return;


        if (index == parts.Length)
        {
            result.Add(current);
            return;
        }


        var part = parts[index];


        if (part.EndsWith("[]"))
        {
            var propertyName = part[..^2];

            if (current[propertyName] is not JsonArray array)
                return;


            foreach (var item in array)
            {
                CollectValues(
                    item,
                    parts,
                    index + 1,
                    result);
            }

            return;
        }


        if (current[part] != null)
        {
            CollectValues(
                current[part],
                parts,
                index + 1,
                result);
        }
    }


    private static void SetValues(
        JsonObject target,
        string path,
        List<JsonNode?> values)
    {
        var parts = path.Split('.');

        var valueIndex = 0;

        ApplyValues(
            target,
            parts,
            0,
            values,
            ref valueIndex);
    }


    private static void ApplyValues(
        JsonNode? current,
        string[] parts,
        int index,
        List<JsonNode?> values,
        ref int valueIndex)
    {
        if (current == null)
            return;


        if (index == parts.Length - 1)
        {
            if (current is JsonObject obj)
            {
                obj[parts[index]] =
                    values[valueIndex++]?.DeepClone();
            }

            return;
        }


        var part = parts[index];


        if (part.EndsWith("[]"))
        {
            var propertyName = part[..^2];


            if (current[propertyName] is not JsonArray array)
                return;


            foreach (var item in array)
            {
                ApplyValues(
                    item,
                    parts,
                    index + 1,
                    values,
                    ref valueIndex);
            }

            return;
        }


        if (current[part] != null)
        {
            ApplyValues(
                current[part],
                parts,
                index + 1,
                values,
                ref valueIndex);
        }
    }
}