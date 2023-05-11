using TimetableBot.Infrastructure;
using TimetableBot.UseCases.Adapters;
using TimetableBot.UseCases.Commands;

namespace TimetableBot.UseCases.CommandHandlers;

public class ShowTimetableCommandHandler : ShowTimetableCommand.IHandler
{
    private readonly ITelegramBotClientAdapter _adapter;
    private readonly IStudyDaysRepository _studyDaysRepository;

    public ShowTimetableCommandHandler(
        ITelegramBotClientAdapter adapter,
        IStudyDaysRepository studyDaysRepository)
    {
        _adapter = adapter;
        _studyDaysRepository = studyDaysRepository;
    }

    public Task HandleAsync(ShowTimetableCommand command)
    {
        var days = _studyDaysRepository
            .GetDays(
                course: command.Course,
                group: command.Group,
                week: command.Week);
        
        return _adapter.ShowTimetableAsync(
            chatId: command.ChatId,
            days: days,
            course: command.Course,
            group: command.Group);
    }
}