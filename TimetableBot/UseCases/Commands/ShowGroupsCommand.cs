using TimetableBot.Models;

namespace TimetableBot.UseCases.Commands;

public class ShowGroupsCommand
{
    public ShowGroupsCommand(List<Group> groups, Student student)
    {
        Groups = groups;
        Student = student;
    }
    
    public interface IHandler
    {
        Task HandleAsync(
            ShowGroupsCommand command);
    }
    public List<Group> Groups { get; }
    
    public Student Student { get; }
}