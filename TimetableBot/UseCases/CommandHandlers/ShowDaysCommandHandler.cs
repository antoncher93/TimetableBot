using TimetableBot.Infrastructure;
using TimetableBot.UseCases.Adapters;
using TimetableBot.UseCases.Commands;

namespace TimetableBot.UseCases.CommandHandlers;

public class ShowDaysCommandHandler : ShowDaysCommand.IHandler
{
    private readonly ITelegramBotClientAdapter _adapter;
    private readonly IStudyDaysRepository _studyDaysRepository;

    public ShowDaysCommandHandler(
        ITelegramBotClientAdapter adapter,
        IStudyDaysRepository studyDaysRepository)
    {
        _adapter = adapter;
        _studyDaysRepository = studyDaysRepository;
    }

    public async Task HandleAsync(ShowDaysCommand command)
    {
        var days = _studyDaysRepository.GetDays(
                course: command.Course,
                group: command.Group,
                week: command.Week)
            .Select(day => day.DayOfWeek)
            .ToList();
        
        await _adapter.ShowDaysAsync(
            student: command.Student,
            course: command.Course,
            group: command.Group,
            week: command.Week,
            days: days);
    }
}