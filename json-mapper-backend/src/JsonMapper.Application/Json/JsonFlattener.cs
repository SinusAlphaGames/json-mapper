using System.Text.Json.Nodes;
using Domain.Json;

namespace JsonMapper.Application.Json;

public class JsonFlattener
{
    public IReadOnlyList<FieldNode> Flatten(JsonNode JsonRoot)
    {
        var result = new List<FieldNode>();
        ProcessNode(JsonRoot, parentPath: "", depth: 0, result);
        return result;
    }

    private void ProcessNode(JsonNode? node, string parentPath, int depth, List<FieldNode> result)
    {
        if (node is null)
            return;

        switch (node)
        {
            case JsonObject jsonObject:
                foreach (var properties in jsonObject)
                {
                    var path = Combine(parentPath, properties.Key);
                    ProcessNode(properties.Value, path, depth + 1, result);
                }
                break;

            case JsonArray jsonArray:
                for (var i = 0; i < jsonArray.Count; i++)
                {
                    var path = $"{parentPath}[{i}]";
                    ProcessNode(jsonArray[i], path, depth + 1, result);
                }
                break;

            default:
                // JsonValue (liść)
                result.Add(new FieldNode(
                    path: parentPath,
                    name: GetLastSegment(parentPath),
                    value: node,
                    depth: depth
                ));
                break;
        }
    }
    
    private static string Combine(string parent, string child)
    {
        return string.IsNullOrEmpty(parent) ? child : $"{parent}.{child}";
    }

    private static string GetLastSegment(string path)
    {
        if (string.IsNullOrEmpty(path))
            return "";

        var dotIndex = path.LastIndexOf('.');
        var bracketIndex = path.LastIndexOf(']');

        var index = Math.Max(dotIndex, bracketIndex);

        return index == -1 ? path : path[(index + 1)..];
    }
}