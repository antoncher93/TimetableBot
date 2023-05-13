using TimetableBot.UseCases.Adapters;
using TimetableBot.UseCases.Commands;

namespace TimetableBot.UseCases.CommandHandlers;

public class ShowTimetableTypesCommandHandler : ShowTimetableTypesCommand.IHandler
{
    private readonly ITelegramBotClientAdapter _adapter;

    public ShowTimetableTypesCommandHandler(ITelegramBotClientAdapter adapter)
    {
        _adapter = adapter;
    }

    public Task HandleAsync(ShowTimetableTypesCommand command)
    {
        return _adapter.ShowTimetableTypesAsync(
            chatId: command.ChatId,
            course: command.Course,
            group: command.Group);
    }
}