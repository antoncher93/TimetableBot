namespace TimetableBot.UseCases.Commands;

public class SendMessageCommand
{
    public SendMessageCommand(
        int messageId,
        long fromChatId,
        string? groupName = default)
    {
        MessageId = messageId;
        FromChatId = fromChatId;
        GroupName = groupName;
    }
    public interface IHandler
    {
        Task HandleAsync(
            SendMessageCommand command);
    }
    
    public int MessageId { get; }
    
    public long FromChatId { get; }
    
    public string? GroupName { get; }
}