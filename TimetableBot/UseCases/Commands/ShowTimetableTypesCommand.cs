namespace TimetableBot.UseCases.Commands;

public class ShowTimetableTypesCommand
{
    public ShowTimetableTypesCommand(long chatId, int course, int group)
    {
        ChatId = chatId;
        Course = course;
        Group = group;
    }
    
    public interface IHandler
    {
        Task HandleAsync(ShowTimetableTypesCommand command);
    }
    
    public long ChatId { get; }
    public int Course { get; }
    public int Group { get; }
}