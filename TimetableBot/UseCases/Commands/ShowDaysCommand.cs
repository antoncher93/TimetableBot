using TimetableBot.CallbackQueries;
using TimetableBot.Models;

namespace TimetableBot.UseCases.Commands;

public class ShowDaysCommand
{
    public ShowDaysCommand(Student student, int course, int group, Week week)
    {
        Student = student;
        Course = course;
        Group = group;
        Week = week;
    }

    public interface IHandler
    {
        Task HandleAsync(
            ShowDaysCommand command);
    }
    public Student Student { get; }
    
    public int Course { get; }
    
    public int Group { get; }
    
    public Week Week { get; }
}