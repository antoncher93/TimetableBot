namespace TimetableBot.Models;

public class Lesson
{
    public Lesson(DateTime startsAt, DateTime endsAt, string title, string description)
    {
        StartsAt = startsAt;
        EndsAt = endsAt;
        Title = title;
        Description = description;
    }
    
    public DateTime StartsAt { get; }
    
    public DateTime EndsAt { get; }
    
    public string Title { get; }
    
    public string Description { get; }
}