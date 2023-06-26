using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
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
                text: "Чтобы посмотреть расписание выберите курс",
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
            })
            .ToList();

        var mainMenuButton = CreateButtonForMainMenu();
        
        buttons.Add(new []{ mainMenuButton });
        
        await _client
            .SendTextMessageAsync(
                chatId: student.ChatId,
                text: "Выберите группу",
                replyMarkup: new InlineKeyboardMarkup(buttons));
    }

    public async Task ShowWeeksAsync(Student student, int course, int group)
    {
        var buttons = new List<InlineKeyboardButton[]>()
        {
            new[]
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
            }
        };
        
        var mainMenuButton = CreateButtonForMainMenu();
        
        buttons.Add(new [] { mainMenuButton });
        
        await _client
            .SendTextMessageAsync(
                chatId: student.ChatId,
                text: "Выберите неделю",
                replyMarkup: new InlineKeyboardMarkup(buttons));
    }

    public async Task ShowTimetableAsync(
        long chatId,
        List<StudyDay> days,
        int course,
        int group)
    {
        var sb = new StringBuilder();

        var button = InlineKeyboardButton.WithCallbackData(
            text: "Назад",
            callbackData: new CallbackDataEnvelope(
                groupTap: new GroupTapCallbackData(
                    course: course,
                    group: group))
                .ToString());

        var buttons = new List<IEnumerable<InlineKeyboardButton>>()
        {
            new[] { button }
        };

        foreach (var day in days)
        {
            sb.AppendLine($"🎓 `{day.DayOfWeek}`");
            if (day.SpecialDescription != null)
            {
                sb.AppendLine(day.SpecialDescription);
            }
            else
            {
                foreach (var lesson in day.Lessons)
                {
                    sb.AppendLine($"🕑{lesson.StartsAt.ToString("hh\\:mm")} - {lesson.EndsAt.ToString("hh\\:mm")}");
                    sb.AppendLine(lesson.Title);
                    sb.AppendLine(lesson.Description);
                }
            }
        }

        var mainMenuButton = CreateButtonForMainMenu();

        buttons.Add(new []{ mainMenuButton });

        await _client.SendTextMessageAsync(
            chatId: chatId,
            text: sb.ToString(),
            parseMode: ParseMode.Markdown,
            replyMarkup: new InlineKeyboardMarkup(buttons));
    }

    public Task SendCopyOfMessageToAllAsync(
        long fromChatId,
        int messageId,
        long chatId)
    {
        return _client.ForwardMessageAsync(
            chatId: chatId,
            messageId: messageId,
            fromChatId: fromChatId);
    }

    public Task SendTokenAsync(long chatId, string token)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Секретный код:");
        sb.AppendLine($"`{token}`");
        sb.AppendLine("Передавайте секретный код только тем, кому хотите предоставить права админа!");
        return _client
            .SendTextMessageAsync(
                chatId: chatId,
                text: sb.ToString(),
                ParseMode.Markdown);
    }

    public Task SendAdminJoinedAsync(Student commandStudent)
    {
        return _client.SendTextMessageAsync(
            chatId: commandStudent.ChatId,
            text: "Теперь вы тоже админ");
    }

    public Task SendCannotJoinAsAdminAsync(Student commandStudent)
    {
        return _client.SendTextMessageAsync(
            chatId: commandStudent.ChatId,
            text: "Некорректный секретный код");
    }

    public async Task ShowTimetableTypesAsync(long chatId, int course, int group)
    {
        var buttons = new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData(
                    text: "На сегодня",
                    callbackData: new CallbackDataEnvelope(
                            timetableTypeTap: new TimetableTypeTap(
                                course: course,
                                group: group,
                                type: TimetableType.Today))
                        .ToString()),
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData(
                    text: "На завтра",
                    callbackData: new CallbackDataEnvelope(
                            timetableTypeTap: new TimetableTypeTap(
                                course: course,
                                group: group,
                                type: TimetableType.Tomorrow))
                        .ToString()),
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData(
                    text: "На неделю",
                    callbackData: new CallbackDataEnvelope(
                            timetableTypeTap: new TimetableTypeTap(
                                course: course,
                                group: group,
                                type: TimetableType.Week))
                        .ToString()),
            },
        };

        await _client.SendTextMessageAsync(
            chatId: chatId,
            text: "Выберите расписание",
            replyMarkup: new InlineKeyboardMarkup(buttons));
    }

    public async Task ShowNoTimetableForDayAsync(long chatId, DateTime date, int course, int group)
    {
        var backButton = InlineKeyboardButton.WithCallbackData(
            text: "Назад",
            callbackData: new CallbackDataEnvelope(
                    groupTap: new GroupTapCallbackData(
                        course: course,
                        group: group))
                .ToString());
        
        var buttons = new IEnumerable<InlineKeyboardButton>[]
        {
            new[] { backButton },
            new [] { this.CreateButtonForMainMenu() },
        };
        
        await _client.SendTextMessageAsync(
            chatId: chatId,
            text: $"Нет рассписания на {date.Date.ToString("yyyy-M-d dddd")}",
            replyMarkup: new InlineKeyboardMarkup(buttons));
    }
    
    public async Task ShowNoTimetableAsync(long chatId, int course, int group)
    {
        var backButton = InlineKeyboardButton.WithCallbackData(
            text: "Назад",
            callbackData: new CallbackDataEnvelope(
                    groupTap: new GroupTapCallbackData(
                        course: course,
                        group: group))
                .ToString());
        
        var buttons = new IEnumerable<InlineKeyboardButton>[]
        {
            new[] { backButton },
            new [] { this.CreateButtonForMainMenu() },
        };
        
        await _client.SendTextMessageAsync(
            chatId: chatId,
            text: $"В настоящий момент занятий нет",
            replyMarkup: new InlineKeyboardMarkup(buttons));
    }

    public async Task ShowDayTimetableAsync(long chatId, DateTime date, StudyDay day, int course, int group)
    {
        var sb = new StringBuilder();
        
        sb.AppendLine($"🎓 `{date.Date.ToString("yyyy-M-d dddd")}`");
        
        if (day.SpecialDescription != null)
        {
            sb.AppendLine(day.SpecialDescription);
        }
        else
        {
            foreach (var lesson in day.Lessons)
            {
                sb.AppendLine($"🕑{lesson.StartsAt.ToString("hh\\:mm")} - {lesson.EndsAt.ToString("hh\\:mm")}");
                sb.AppendLine(lesson.Title);
                sb.AppendLine(lesson.Description);
            }
        }

        var backButton = InlineKeyboardButton.WithCallbackData(
            text: "Назад",
            callbackData: new CallbackDataEnvelope(
                groupTap: new GroupTapCallbackData(
                    course: course,
                    group: group))
                .ToString());

        var buttons = new IEnumerable<InlineKeyboardButton>[]
        {
            new[] { backButton },
            new []{ CreateButtonForMainMenu() },
        };
        
        await _client.SendTextMessageAsync(
            chatId: chatId,
            text: sb.ToString(),
            parseMode: ParseMode.Markdown,
            replyMarkup: new InlineKeyboardMarkup(buttons));
    }

    public Task SendAdminHasBeenDeletedAsync(
        long chatId)
    {
        return _client.SendTextMessageAsync(
            chatId: chatId,
            text: "Скретный ключ был удален. Связанный с ним пользователь потерял права администратора.");
    }

    public Task NotifyMessageWasSentAsync(long chatId)
    {
        return _client.SendTextMessageAsync(
            chatId: chatId,
            text: "Сообщение было отправлено");
    }

    public Task SendGroupDoesNotExistsAsync(long chatId, string groupName)
    {
        return _client.SendTextMessageAsync(
            chatId: chatId,
            text: $"Не удалось найти группу {groupName}");
    }

    private InlineKeyboardButton CreateButtonForMainMenu()
    {
       return InlineKeyboardButton.WithCallbackData(
           text: "Главное меню",
           callbackData: new CallbackDataEnvelope(
                   mainMenu: new MainMenuCallbackData())
               .ToString());
    }
}