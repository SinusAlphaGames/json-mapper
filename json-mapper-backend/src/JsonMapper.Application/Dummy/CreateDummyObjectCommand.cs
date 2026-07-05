using MediatR;

namespace JsonMapper.Application.Dummy;

public record CreateDummyObjectCommand(string Name) : IRequest<Guid>;