namespace Domain.Mappings;

public class JsonFieldMapping
{
    public Guid Id { get; set; }

    public Guid JsonMappingId { get; set; }

    public JsonMapping Mapping { get; set; } = null!;

    public string SourceField { get; set; } = string.Empty;

    public string TargetField { get; set; } = string.Empty;
}