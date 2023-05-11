using TimetableBot.Models;

namespace TimetableBot.UseCases.Commands;

public class AddAdminCommand
{
    public AddAdminCommand(long chatId)
    {
        ChatId = chatId;
    }
    
    public interface IHandler
    {
        Task HandleAsync(AddAdminCommand command);
    }
    public long ChatId { get; }
}