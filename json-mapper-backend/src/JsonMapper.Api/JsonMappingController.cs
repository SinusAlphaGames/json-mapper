using JsonMapper.Application.Json;
using JsonMapper.Application.Json.save;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace json_mapper_backend;

[ApiController]
[Route("api/mapping/json")]
public class JsonMappingController(IMediator mediator) : ControllerBase
{
    
    [HttpPost]
    public async Task<IActionResult> CreateJsonMapping(CreateJsonMappingCommand command)
    {
        var createJsonMappingResult = await mediator.Send(command);
        return Ok(createJsonMappingResult);
    }
    
    [HttpPost("save")]
    public async Task<IActionResult> SaveJsonMapping(SaveJsonMappingCommand command)
    {
        await mediator.Send(command);
        return Ok();
    }
}