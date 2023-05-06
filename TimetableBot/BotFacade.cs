using Telegram.Bot.Hosting;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TimetableBot.UseCases.Commands;
using TimetableBot.UseCases.Queries;

namespace TimetableBot;

public class BotFacade : IBotFacade
{
    private readonly RegisterStudentQuery.IHandler _registerStudentQueryHandler;
    private readonly ICoursesQuery _coursesQuery;
    private readonly ShowCoursesCommand.IHandler _showCoursesCommandHandler;

    public BotFacade(
        ICoursesQuery coursesQuery,
        RegisterStudentQuery.IHandler registerStudentQueryHandler,
        ShowCoursesCommand.IHandler showCoursesCommandHandler)
    {
        _coursesQuery = coursesQuery;
        _registerStudentQueryHandler = registerStudentQueryHandler;
        _showCoursesCommandHandler = showCoursesCommandHandler;
    }

    public Task OnUpdateAsync(Update update)
    {
        return update.Type switch
        {
            UpdateType.Message => this.HandleMessageAsync(
                message: update.Message!),
            _ => Task.CompletedTask,
        };
    }

    private Task HandleMessageAsync(Message message)
    {
        if(message.Text == "/start")
        {
            return this.OnStartBotCommandAsync(
                userId: message.From!.Id,
                chatId: message.Chat.Id);
        }

        return Task.CompletedTask;
    }

    private async Task OnStartBotCommandAsync(
        long userId,
        long chatId)
    {
        var student = await _registerStudentQueryHandler
            .HandleAsync(
                query: new RegisterStudentQuery(
                    userId: userId,
                    chatId: chatId));
        
        var courses = await _coursesQuery.GetCoursesAsync();

        await _showCoursesCommandHandler
            .HandleAsync(
                command: new ShowCoursesCommand(
                    student: student,
                    courses: courses));
    }
}