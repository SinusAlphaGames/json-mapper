namespace Domain.Mappings;

public class JsonMapping
{
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public List<JsonFieldMapping> Fields { get; set; } = [];
}