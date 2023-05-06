namespace TimetableBot.Models;

public class Course
{
    public Course(
        string name,
        List<Group> groups)
    {
        Name = name;
        Groups = groups;
    }
    
    public string Name { get; }
    
    public IReadOnlyList<Group> Groups { get; }
}