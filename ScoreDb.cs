using Microsoft.EntityFrameworkCore;

public class ScoreDb : DbContext
{
    public ScoreDb(DbContextOptions<ScoreDb> options) : base(options) { }

    public DbSet<Score> Scores => Set<Score>();
}
