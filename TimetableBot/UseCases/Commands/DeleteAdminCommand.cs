namespace TimetableBot.UseCases.Commands;

public class DeleteAdminCommand
{
    public DeleteAdminCommand(string token, long chatId)
    {
        Token = token;
        ChatId = chatId;
    }
    
    public interface IHandler
    {
        Task HandleAsync(
            DeleteAdminCommand command);
    }
    
    public long ChatId { get; }
    
    public string Token { get; }
}