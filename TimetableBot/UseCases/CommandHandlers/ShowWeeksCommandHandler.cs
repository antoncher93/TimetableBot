using TimetableBot.UseCases.Adapters;
using TimetableBot.UseCases.Commands;

namespace TimetableBot.UseCases.CommandHandlers;

public class ShowWeeksCommandHandler : ShowWeeksCommand.IHandler
{
    private readonly ITelegramBotClientAdapter _adapter;

    public ShowWeeksCommandHandler(ITelegramBotClientAdapter adapter)
    {
        _adapter = adapter;
    }

    public Task HandleAsync(ShowWeeksCommand command)
    {
        return _adapter.ShowWeeksAsync(
            student: command.Student,
            course: command.Course,
            group: command.Group);
    }
}