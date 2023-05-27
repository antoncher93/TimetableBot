using TimetableBot.Models;

namespace TimetableBot.UseCases.Commands;

public class JoinAdminCommand
{
    public JoinAdminCommand(Student student, string token)
    {
        Student = student;
        Token = token;
    }

    public interface IHandler
    {
        Task HandleAsync(JoinAdminCommand adminCommand);
    }
    
    public Student Student { get; }
    
    public string Token { get; }
}