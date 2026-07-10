using System.Text.Json.Nodes;
using Domain.Json;

namespace JsonMapper.Application.Json;

public static class JsonFlattener
{
    public static IReadOnlyList<FieldNode> Flatten(JsonNode JsonRoot)
    {
        var result = new Dictionary<string, FieldNode>();
        ProcessNode(JsonRoot, parentPath: "", depth: 0, result);
        return result.Values.ToList();
    }

    private static void ProcessNode(JsonNode? node, string parentPath, int depth, Dictionary<string, FieldNode> result)
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
                
                if (jsonArray.Count == 0)
                    break;

                ProcessArray(node, parentPath, depth, result, jsonArray);
                break;

            default:
                // JsonValue (liść)
                if (!result.ContainsKey(parentPath))
                {
                    result.Add(parentPath, new FieldNode(
                        path: parentPath,
                        name: GetLastSegment(parentPath),
                        value: node,
                        depth: depth
                    ));    
                }
                break;
        }
    }

    private static void ProcessArray(JsonNode node, string parentPath, int depth, Dictionary<string, FieldNode> result, JsonArray jsonArray)
    {
        var first = jsonArray[0];

        if (first is JsonObject)
        {
            foreach (var item in jsonArray)
            {
                ProcessNode(
                    item,
                    $"{parentPath}[]",
                    depth + 1,
                    result
                );
            }
        }
        else
        {
            if (!result.ContainsKey(parentPath))
            {
                result.Add(parentPath, new FieldNode(
                    path: parentPath,
                    name: GetLastSegment(parentPath),
                    value: node,
                    depth: depth
                ));    
            }
            
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