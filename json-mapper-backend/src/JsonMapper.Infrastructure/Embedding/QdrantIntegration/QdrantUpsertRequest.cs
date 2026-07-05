namespace JsonMapper.Infrastructure.Embedding.QdrantIntegration;

public class QdrantUpsertRequest
{
    public List<QdrantPoint> Points { get; set; } = new();
}
