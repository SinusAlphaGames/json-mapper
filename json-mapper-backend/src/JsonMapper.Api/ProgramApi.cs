using JsonMapper.Application;
using JsonMapper.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureLayer();

var app = builder.Build();

app.MapControllers();

app.Run();