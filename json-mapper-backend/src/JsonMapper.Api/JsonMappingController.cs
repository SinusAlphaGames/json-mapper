using JsonMapper.Application.Json;
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
}