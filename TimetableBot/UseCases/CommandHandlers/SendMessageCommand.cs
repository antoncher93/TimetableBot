namespace TimetableBot.UseCases.CommandHandlers;

public class SendMessageCommand
{
    public SendMessageCommand(int messageId, long fromChatId)
    {
        MessageId = messageId;
        FromChatId = fromChatId;
    }
    public interface IHandler
    {
        Task HandleAsync(SendMessageCommand command);
    }
    
    public int MessageId { get; }
    public long FromChatId { get; }
}