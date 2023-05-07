namespace TimetableBot.Models;

public class Group
{
    public Group(
        string name,
        StudyWeek week1,
        StudyWeek week2)
    {
        Name = name;
        Week1 = week1;
        Week2 = week2;
    }
    public string Name { get; }
    
    public StudyWeek Week1 { get; }
    
    public StudyWeek Week2 { get; }
}