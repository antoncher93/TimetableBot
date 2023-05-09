using Newtonsoft.Json;
using Telegram.Bot.Hosting;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TimetableBot.CallbackQueries;
using TimetableBot.Models;
using TimetableBot.UseCases;
using TimetableBot.UseCases.CommandHandlers;
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
    private readonly ShowWeeksCommand.IHandler _showWeeksCommandHandler;
    private readonly ShowTimetableCommand.IHandler _showTimetableCommandHandler;
    private readonly ShowDaysCommandHandler _showDaysCommandHandler;

    public BotFacade(
        ICoursesQuery coursesQuery,
        RegisterStudentQuery.IHandler registerStudentQueryHandler,
        ShowCoursesCommand.IHandler showCoursesCommandHandler,
        GroupsQuery.IHandler groupsQueryHandler,
        ShowGroupsCommand.IHandler showGroupsCommandHandler,
        ShowWeeksCommand.IHandler showWeeksCommandHandler,
        ShowDaysCommandHandler showDaysCommandHandler, 
        ShowTimetableCommand.IHandler showTimetableCommandHandler)
    {
        _coursesQuery = coursesQuery;
        _registerStudentQueryHandler = registerStudentQueryHandler;
        _showCoursesCommandHandler = showCoursesCommandHandler;
        _groupsQueryHandler = groupsQueryHandler;
        _showGroupsCommandHandler = showGroupsCommandHandler;
        _showWeeksCommandHandler = showWeeksCommandHandler;
        _showDaysCommandHandler = showDaysCommandHandler;
        _showTimetableCommandHandler = showTimetableCommandHandler;
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

    private async Task HandleCallbackQueryAsync(CallbackQuery callbackQuery)
    {
        var callbackDataEnvelope = JsonConvert.DeserializeObject<CallbackDataEnvelope>(callbackQuery.Data);

        var userId = callbackQuery.From.Id;
        var chatId = callbackQuery.Message!.Chat.Id;

        var student = await this.RegisterStudentAsync(
            userId: userId,
            chatId: chatId);
        
        await callbackDataEnvelope.Match(
            onCourseTap: courseTap => this.OnCourseTapCallback(
                courseTap: courseTap,
                student: student),
            onGroupTap: groupTap => this.OnGroupTapAsync(
                 groupTap: groupTap,
                 student: student),
            onWeekTap: weekTap => this.OnWeekTapAsync(
                student: student,
                weekTap: weekTap),
            onDayTap: dayTap => this.OnDayTap());

    }

    private Task OnDayTap()
    {
        return Task.CompletedTask;
    }

    private Task OnWeekTapAsync(
        Student student,
        WeekTapCallbackData weekTap)
    {
        return _showTimetableCommandHandler
            .HandleAsync(
                command: new ShowTimetableCommand(
                    chatId: student.ChatId,
                    course: weekTap.Course,
                    group: weekTap.Group,
                    week: weekTap.Week));
    }

    private Task OnGroupTapAsync(
        Student student,
        GroupTapCallbackData groupTap)
    {
        return _showWeeksCommandHandler
            .HandleAsync(
                command: new ShowWeeksCommand(
                    student: student,
                    course: groupTap.Course,
                    groupTap.Group));
    }

    private async Task OnCourseTapCallback(
        Student student,
        CourseTapCallbackData courseTap)
    {
        var groups = await _groupsQueryHandler
            .HandleAsync(
                query: new GroupsQuery(
                    course: courseTap.Course));

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

    private Task<Student> RegisterStudentAsync(
        long userId,
        long chatId)
    {
        return _registerStudentQueryHandler
            .HandleAsync(
                query: new RegisterStudentQuery(
                    userId: userId,
                    chatId: chatId));
    }
}