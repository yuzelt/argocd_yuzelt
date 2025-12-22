using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Minimal API + Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger sadece dev’de açık
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Basit root endpoint (release testi)
app.MapGet("/", () => "Lab microservice is running - release v1.2.19");

// Health endpoint
app.MapGet("/health", () =>
{
    return Results.Ok(new
    {
        status = "UP",
        env = app.Environment.EnvironmentName,
        time = DateTime.UtcNow
    });
});

// Hello endpoint
app.MapGet("/hello", ([FromQuery] string? name) =>
{
    var n = string.IsNullOrWhiteSpace(name) ? "world" : name;

    return Results.Ok(new
    {
        message = $"Hello {n} from lab microservice 👋",
        time = DateTime.UtcNow
    });
});

app.Run();
