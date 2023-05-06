using TimetableBot.UseCases.Adapters;
using TimetableBot.UseCases.Commands;

namespace TimetableBot.UseCases.CommandHandlers;

public class ShowCoursesCommandHandler : ShowCoursesCommand.IHandler
{
    private readonly ITelegramBotClientAdapter _adapter;

    public ShowCoursesCommandHandler(
        ITelegramBotClientAdapter adapter)
    {
        _adapter = adapter;
    }

    public Task HandleAsync(ShowCoursesCommand command)
    {
        return _adapter.ShowCoursesAsync(
            student: command.Student,
            courses: command.Courses);
    }
}