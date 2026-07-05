namespace JsonMapper.Infrastructure.Embedding.QdrantIntegration.Search;

public class QdrantSearchRequest
{
    public float[] Vector {get; set; }
    public int Limit { get; set; }
    public bool WithPayload { get; set; }
}