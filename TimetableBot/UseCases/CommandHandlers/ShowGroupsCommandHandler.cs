using TimetableBot.UseCases.Adapters;
using TimetableBot.UseCases.Commands;

namespace TimetableBot.UseCases.CommandHandlers;

public class ShowGroupsCommandHandler : ShowGroupsCommand.IHandler
{
    private readonly ITelegramBotClientAdapter _adapter;

    public ShowGroupsCommandHandler(ITelegramBotClientAdapter adapter)
    {
        _adapter = adapter;
    }

    public Task HandleAsync(
        ShowGroupsCommand command)
    {
        return _adapter.ShowGroupsAsync(
            student: command.Student,
            groups: command.Groups, 
            course: command.Course);
    }
}