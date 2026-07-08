namespace JsonMapper.Application.Json.get;

public class MappingDto
{
    public string SourceField { get; set; } = string.Empty;

    public string TargetField { get; set; } = string.Empty;

    public double? Score { get; set; }
}