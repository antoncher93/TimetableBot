using TimetableBot.Models;

namespace TimetableBot.UseCases.Commands;

public class JoinCommand
{
    public JoinCommand(Student student, string token)
    {
        Student = student;
        Token = token;
    }

    public interface IHandler
    {
        Task HandleAsync(JoinCommand command);
    }
    
    public Student Student { get; }
    
    public string Token { get; }
}