using TimetableBot.Models;

namespace TimetableBot.UseCases.Commands;

public class ShowCoursesCommand
{
    public ShowCoursesCommand(Student student, List<string> courses)
    {
        Courses = courses;
        Student = student;
    }
    
    public interface IHandler
    {
        Task HandleAsync(
            ShowCoursesCommand command);
    }
    
    public Student Student { get; }
    
    public List<string> Courses { get; }
}