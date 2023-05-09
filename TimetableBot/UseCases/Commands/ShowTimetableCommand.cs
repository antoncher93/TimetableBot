using TimetableBot.CallbackQueries;
using TimetableBot.Models;

namespace TimetableBot.UseCases.Commands;

public class ShowTimetableCommand
{
    public ShowTimetableCommand(long chatId, int course, int group, Week week)
    {
        ChatId = chatId;
        Course = course;
        Group = group;
        Week = week;
    }
    
    public interface IHandler
    {
        Task HandleAsync(ShowTimetableCommand command);
    }
    
    public long ChatId { get; }
    public int Course { get; }
    public int Group { get; }
    public Week Week { get; }
}