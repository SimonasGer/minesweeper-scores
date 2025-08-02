using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ScoreDb>(options =>
    options.UseSqlite("Data Source=scores.db"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendOnly", policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5500")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});



var app = builder.Build();

// Ensure DB file exists and schema is applied
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ScoreDb>();
    db.Database.Migrate();
}

// GET top 5 scores
app.MapGet("/scores", async (ScoreDb db) =>
    await db.Scores.OrderBy(score => score.Date).Take(5).ToListAsync());

// POST a new score
app.MapPost("/scores", async (Score newScore, ScoreDb db) =>
{
    db.Scores.Add(newScore);
    await db.SaveChangesAsync();
    return Results.Ok();
});

app.UseCors("FrontendOnly");

app.Run();
