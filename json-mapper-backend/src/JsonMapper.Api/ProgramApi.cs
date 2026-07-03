using JsonMapper.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddApplicationLayer();

var app = builder.Build();

app.MapControllers();

app.Run();