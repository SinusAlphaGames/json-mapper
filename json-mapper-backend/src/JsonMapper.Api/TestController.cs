using JsonMapper.Application;
using Microsoft.AspNetCore.Mvc;

namespace json_mapper_backend;

[ApiController]
[Route("api/test")]
public class TestController(TestService testService) : ControllerBase
{
    [HttpGet]
    public IActionResult GetTest()
    {
        var result = testService.GetTest();
        return Ok(result);
    }
}