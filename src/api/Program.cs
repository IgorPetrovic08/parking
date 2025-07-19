var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.MapGet("/hello", () => Results.Text("hello world", "text/plain"));

app.MapPost("/register", (RegistrationRequest request, ILogger<Program> logger) =>
{
    logger.LogInformation("Received registration: Plates={Plates}, CityId={CityId}, ZoneId={ZoneId}", request.RegistrationPlates, request.CityId, request.ZoneId);
    return Results.Ok(new { message = "Registration received" });
});

app.Run();

record RegistrationRequest(string RegistrationPlates, int CityId, int ZoneId);
