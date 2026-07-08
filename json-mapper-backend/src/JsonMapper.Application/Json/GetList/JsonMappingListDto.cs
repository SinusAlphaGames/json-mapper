using JsonMapper.Application.Json.get;

namespace JsonMapper.Application.Json.GetList;

public class JsonMappingListDto
{
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public List<MappingDto> Mappings { get; set; } = [];
}