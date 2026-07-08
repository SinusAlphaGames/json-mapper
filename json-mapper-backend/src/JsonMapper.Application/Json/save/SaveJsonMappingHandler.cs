using Domain.Mappings;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JsonMapper.Application.Json.save;

public class SaveJsonMappingHandler(
        IJsonMappingRepository repository,
        ILogger<SaveJsonMappingHandler> logger
        ) : IRequestHandler<SaveJsonMappingCommand>
{
    public async Task Handle(
        SaveJsonMappingCommand request, CancellationToken cancellationToken)
    {
        var mapping = new JsonMapping()
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,

            Fields = request.Mappings
                .Select(x => new JsonFieldMapping
                {
                    Id = Guid.NewGuid(),
                    SourceField = x.SourceField,
                    TargetField = x.TargetField
                })
                .ToList()
        };


        await repository.AddAsync(mapping, cancellationToken);


        logger.LogInformation(
            "JSON mapping saved {MappingId}", mapping.Id);
    }
}