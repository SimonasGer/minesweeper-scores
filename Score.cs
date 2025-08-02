public class Score
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public int Bombs { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
}