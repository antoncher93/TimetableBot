using TimetableBot.Models;

namespace TimetableBot.UseCases.Queries;

public class RegisterStudentQuery
{
    public RegisterStudentQuery(long userId, long chatId)
    {
        UserId = userId;
        ChatId = chatId;
    }
    
    public interface IHandler
    {
        Task<Student> HandleAsync(
            RegisterStudentQuery query);
    }
    
    public long UserId { get; }
    
    public long ChatId { get; }
}