namespace JsonMapper.Infrastructure.Embedding.QdrantIntegration;

public class QdrantPoint
{
    public long Id { get; set; }
    public float[] Vector { get; set; }
    public object Payload { get; set; }
}