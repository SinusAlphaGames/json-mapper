namespace JsonMapper.Infrastructure.Embedding.QdrantIntegration.Search;

public class QdrantSearchedValue
{
    public int Id {get; set; }
    public float Score { get; set; }
    public QdrantPointPayload Payload { get; set; }
}