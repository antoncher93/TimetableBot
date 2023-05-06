namespace TimetableBot.Models;

public class Course
{
    public Course(string name)
    {
        Name = name;
    }
    
    public string Name { get; }
}