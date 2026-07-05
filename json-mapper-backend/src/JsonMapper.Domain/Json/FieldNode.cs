using System.Text.Json.Nodes;

namespace Domain.Json;

public class FieldNode(string path, string name, JsonNode value, int depth)
{
    public string Path { get; set; } = path;
    public string Name { get; set; } = name;
    public JsonNode Value { get; set; } = value;

    public int Depth { get; init; } = depth;

    public List<FieldNode> Children { get; set; } = [];
}