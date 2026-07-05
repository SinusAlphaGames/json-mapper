using JsonMapper.Application;
using JsonMapper.Application.Dummy;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace json_mapper_backend;

[ApiController]
[Route("api/test")]
public class TestController(IMediator mediator) : ControllerBase
{
    
    [HttpPost]
    public async Task<IActionResult> CreateDummyObject(CreateDummyObjectCommand command)
    {
        var id = await mediator.Send(command);
        return Ok(id);
    }
}