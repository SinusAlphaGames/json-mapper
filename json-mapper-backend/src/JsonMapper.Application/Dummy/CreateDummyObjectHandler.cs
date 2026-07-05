using Domain.Dummy;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JsonMapper.Application.Dummy;

public class CreateDummyObjectHandler(ILogger<CreateDummyObjectHandler> logger)
    : IRequestHandler<CreateDummyObjectCommand, Guid>
{
    private readonly ILogger<CreateDummyObjectHandler> _logger = logger;

    public Task<Guid> Handle(CreateDummyObjectCommand request, CancellationToken cancellationToken)
    {
        var dummy = new DummyObject(request.Name);
        _logger.LogInformation("Dummy object created: {Id}", dummy.Id);
        return Task.FromResult(dummy.Id);
    }
}