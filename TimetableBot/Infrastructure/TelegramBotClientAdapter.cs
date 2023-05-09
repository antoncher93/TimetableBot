using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using TimetableBot.CallbackQueries;
using TimetableBot.Models;
using TimetableBot.UseCases.Adapters;

namespace TimetableBot.Infrastructure;

public class TelegramBotClientAdapter : ITelegramBotClientAdapter
{
    private readonly ITelegramBotClient _client;

    public TelegramBotClientAdapter(ITelegramBotClient client)
    {
        _client = client;
    }

    public async Task ShowCoursesAsync(
        Student student,
        List<string> courses)
    {
        
        var buttons = courses
            .Select((course, i) =>
            {
                var callbackData = new CallbackDataEnvelope(
                        courseTap: new CourseTapCallbackData(course: i))
                    .ToString();

                return new[]
                {
                    InlineKeyboardButton.WithCallbackData(
                        text: course,
                        callbackData: callbackData),
                };
            });
        
        await _client
            .SendTextMessageAsync(
                chatId: student.ChatId,
                text: "Чтобы посмотреть рассписание выберите курс",
                replyMarkup: new InlineKeyboardMarkup(buttons));
    }

    public async Task ShowGroupsAsync(Student student,
        List<Group> groups,
        int course)
    {
        var buttons = groups
            .Select((group, i) =>
            {
                var data = new CallbackDataEnvelope(
                        groupTap: new GroupTapCallbackData(
                            group: i, 
                            course: course))
                    .ToString();

                return new[]
                {
                    InlineKeyboardButton.WithCallbackData(
                        text: group.Name,
                        callbackData: data),
                };
            });
        
        await _client
            .SendTextMessageAsync(
                chatId: student.ChatId,
                text: "Выберите группу",
                replyMarkup: new InlineKeyboardMarkup(buttons));
    }

    public async Task ShowWeeksAsync(Student student, int course, int group)
    {
        var buttons = new InlineKeyboardButton[]
        {
            InlineKeyboardButton.WithCallbackData(
                text: "1",
                callbackData: new CallbackDataEnvelope(
                        weekTap: new WeekTapCallbackData(
                            course: course,
                            group: group,
                            week: Week.First))
                    .ToString()),
            InlineKeyboardButton.WithCallbackData(
                text: "2",
                callbackData: new CallbackDataEnvelope(
                        weekTap: new WeekTapCallbackData(
                            course: course,
                            group: group,
                            week: Week.Second))
                    .ToString())
        };
        
        await _client
            .SendTextMessageAsync(
                chatId: student.ChatId,
                text: "Выберите неделю",
                replyMarkup: new InlineKeyboardMarkup(buttons));
    }

    public async Task ShowDaysAsync(Student student,
        int course,
        int group,
        Week week,
        List<string> days)
    {
        var buttons = days
            .Select((day, i) =>
            {
                var data = new CallbackDataEnvelope(
                        dayTap: new DayTapCallbackData(
                            course: course,
                            group: group,
                            week: week,
                            dayOfWeek: i))
                    .ToString();

                return new[]
                {
                    InlineKeyboardButton.WithCallbackData(
                        text: day,
                        callbackData: data),
                };
            });

        var maxLength = buttons
            .SelectMany(b => b)
            .Select(b => b.CallbackData.Length)
            .Max();

        await _client.SendTextMessageAsync(
            chatId: student.ChatId,
            text: "Выберите день",
            replyMarkup: new InlineKeyboardMarkup(buttons));
    }
}