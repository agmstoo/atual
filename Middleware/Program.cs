using Microsoft.Extensions.Logging;
using Middleware.Middleware;
using Middleware.Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<RequestMiddleware>();
builder.Services.AddTransient<IMainService, MainService>();
builder.Services.AddHostedService<ProcessService>();

var app = builder.Build();
app.UseErrorHandler();
app.UseMiddleware<RequestMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
