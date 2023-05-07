using Newtonsoft.Json;
using Telegram.Bot.Hosting;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TimetableBot.CallbackQueries;
using TimetableBot.UseCases;
using TimetableBot.UseCases.Commands;
using TimetableBot.UseCases.Queries;

namespace TimetableBot;

public class BotFacade : IBotFacade
{
    private readonly RegisterStudentQuery.IHandler _registerStudentQueryHandler;
    private readonly ICoursesQuery _coursesQuery;
    private readonly GroupsQuery.IHandler _groupsQueryHandler;
    private readonly ShowCoursesCommand.IHandler _showCoursesCommandHandler;
    private readonly ShowGroupsCommand.IHandler _showGroupsCommandHandler;

    public BotFacade(
        ICoursesQuery coursesQuery,
        RegisterStudentQuery.IHandler registerStudentQueryHandler,
        ShowCoursesCommand.IHandler showCoursesCommandHandler,
        GroupsQuery.IHandler groupsQueryHandler, ShowGroupsCommand.IHandler showGroupsCommandHandler)
    {
        _coursesQuery = coursesQuery;
        _registerStudentQueryHandler = registerStudentQueryHandler;
        _showCoursesCommandHandler = showCoursesCommandHandler;
        _groupsQueryHandler = groupsQueryHandler;
        _showGroupsCommandHandler = showGroupsCommandHandler;
    }

    public Task OnUpdateAsync(Update update)
    {
        return update.Type switch
        {
            UpdateType.Message => this.HandleMessageAsync(
                message: update.Message!),
            UpdateType.CallbackQuery => this.HandleCallbackQueryAsync(
                callbackQuery: update.CallbackQuery!),
            _ => Task.CompletedTask,
        };
    }

    private Task HandleCallbackQueryAsync(CallbackQuery callbackQuery)
    {
        var callbackQueryEnvelope = JsonConvert.DeserializeObject<CallbackQueryEnvelope>(callbackQuery.Data!);

        return callbackQueryEnvelope.Match(
            onCourseTap: courseTap => this.OnCourseTapCallback(
                courseTap: courseTap,
                userId: callbackQuery.From.Id,
                chatId: callbackQuery.Message!.Chat.Id),
            onGroupTap: groupTap => this.OnGroupTap(groupTap));

    }

    private Task OnGroupTap(
        GroupTapCallbackQuery groupTap)
    {
        return Task.CompletedTask;
    }

    private async Task OnCourseTapCallback(
        CourseTapCallbackQuery courseTap,
        long userId,
        long chatId)
    {
        var groups = await _groupsQueryHandler
            .HandleAsync(
                query: new GroupsQuery(
                    course: courseTap.Course));

        var student = await _registerStudentQueryHandler
            .HandleAsync(
                query: new RegisterStudentQuery(
                    userId: userId,
                    chatId: chatId));

        await _showGroupsCommandHandler
            .HandleAsync(
                command: new ShowGroupsCommand(
                    course: courseTap.Course,
                    groups: groups,
                    student: student));
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