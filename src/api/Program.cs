
using api.Models;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

// Register DbContext with SQLite
builder.Services.AddDbContext<api.Data.ParkingDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}



app.MapGet("/registrations/{plates}", async (string plates, api.Data.ParkingDbContext db) =>
{
    var today = DateTime.UtcNow.Date;
    var tomorrow = today.AddDays(1);
    var results = await db.RegistrationRequests
        .Where(r => r.RegistrationPlates == plates && r.RegisteredAt >= today && r.RegisteredAt < tomorrow)
        .ToListAsync();
    return Results.Ok(results);
});

app.MapPost("/register", async (RegistrationRequest request, api.Data.ParkingDbContext db, ILogger<Program> logger) =>
{
    request.RegisteredAt = DateTime.UtcNow;
    db.RegistrationRequests.Add(request);
    await db.SaveChangesAsync();
    logger.LogInformation("Received registration: Plates={Plates}, CityId={CityId}, ZoneId={ZoneId}", request.RegistrationPlates, request.CityId, request.ZoneId);
    return Results.Ok(new { message = "Registration received" });
});

app.Run();
