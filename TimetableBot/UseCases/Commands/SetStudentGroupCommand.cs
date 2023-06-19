using TimetableBot.Models;

namespace TimetableBot.UseCases.Commands;

public class SetStudentGroupCommand
{
    public SetStudentGroupCommand(Student student, int courseIndex, int groupIndex)
    {
        Student = student;
        CourseIndex = courseIndex;
        GroupIndex = groupIndex;
    }

    public interface IHandler
    {
        Task HandleAsync(
            SetStudentGroupCommand command);
    }
    
    public Student Student { get; }
    
    public int CourseIndex { get; }
    
    public int GroupIndex { get; }
}