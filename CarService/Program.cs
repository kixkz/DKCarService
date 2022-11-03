using System.Reflection;
using CarService.Extensions;
using CarService.HealtChecks;
using CarService.Models.MediatR;
using MediatR;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.RegisterRepositories()
    .RegisterServices()
    .AddAutoMapper(typeof(Program));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
    .CreateLogger();

builder.Services.AddHealthChecks()
    .AddCheck<SqlHealthCheck>("SQL Server")
    .AddUrlGroup(new Uri("Https://google.bg"), name: "Google Service")
    .AddCheck<CustomHealthCheck>("Server OK");

builder.Services.AddMediatR(typeof(AddCarCommand).Assembly);


builder.Logging.AddSerilog(logger);

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapHealthChecks("/health");

app.RegisterHealthChecks();

app.MapControllers();

app.Run();
