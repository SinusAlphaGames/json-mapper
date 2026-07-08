using MediatR;
using Microsoft.Extensions.Logging;

namespace JsonMapper.Application.Json.save;

public class SaveJsonMappingHandler(ILogger<SaveJsonMappingHandler> logger) : IRequestHandler<SaveJsonMappingCommand>
{
    public async Task Handle(
        SaveJsonMappingCommand request, CancellationToken cancellationToken)
    {
        foreach (var mapping in request.Mappings)
        {
            // TODO: zapis do bazy
            logger.LogInformation(
                "Mapping saved: {SourceField} -> {TargetField}", mapping.SourceField, mapping.TargetField);
        }

        await Task.CompletedTask;
    }
    
}