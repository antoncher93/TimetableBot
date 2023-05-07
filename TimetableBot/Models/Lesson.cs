namespace TimetableBot.Models;

public class Lesson
{
    public Lesson(
        TimeSpan startsAt,
        TimeSpan endsAt,
        string title,
        string description)
    {
        StartsAt = startsAt;
        EndsAt = endsAt;
        Title = title;
        Description = description;
    }
    
    public TimeSpan StartsAt { get; }
    
    public TimeSpan EndsAt { get; }
    
    public string Title { get; }
    
    public string Description { get; }
}