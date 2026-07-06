using JsonMapper.Infrastructure.Embedding.QdrantIntegration.Search;

namespace JsonMapper.Infrastructure.Embedding.QdrantIntegration;

public class QdrantPoint
{
    public Guid Id { get; set; }
    public float[] Vector { get; set; }
    public QdrantPointPayload Payload { get; set; }
}