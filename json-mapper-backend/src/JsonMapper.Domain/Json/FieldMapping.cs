namespace Domain.Json;

public class FieldMapping
{
    public string SourceField { get; set; } = null!;
    public string TargetField { get; set; } = null!;
    public double Score { get; set; }
}