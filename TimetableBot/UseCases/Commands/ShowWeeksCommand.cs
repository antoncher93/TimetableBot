using TimetableBot.Models;

namespace TimetableBot.UseCases.Commands;

public class ShowWeeksCommand
{
    public ShowWeeksCommand(Student student, int course, int group)
    {
        Student = student;
        Course = course;
        Group = group;
    }
    
    public interface IHandler
    {
        Task HandleAsync(
            ShowWeeksCommand command);
    }
    public Student Student { get; }
    public int Course { get; }
    public int Group { get; }
}