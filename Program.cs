using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ScoreDb>(options =>
    options.UseSqlite("Data Source=scores.db"));

var app = builder.Build();

// Ensure DB file exists and schema is applied
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ScoreDb>();
    db.Database.Migrate();
}

// GET top 10 scores (lowest time is best)
app.MapGet("/scores", async (ScoreDb db) =>
    await db.Scores.OrderBy(score => score.Date).Take(5).ToListAsync());

// POST a new score
app.MapPost("/scores", async (Score newScore, ScoreDb db) =>
{
    db.Scores.Add(newScore);
    await db.SaveChangesAsync();
    return Results.Ok();
});

app.Run();
