using JsonMapper.Infrastructure.Embedding.QdrantIntegration.Search;

namespace JsonMapper.Infrastructure.Embedding.QdrantIntegration;

public class QdrantPoint
{
    public long Id { get; set; }
    public float[] Vector { get; set; }
    public QdrantPointPayload Payload { get; set; }
}