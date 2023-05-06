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
            .Select(course =>
            {
                var callbackData = new CallbackQueryEnvelope(
                        courseTap: new CourseTapCallbackQuery(course: course))
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
                text: "Выберите курс:",
                replyMarkup: new InlineKeyboardMarkup(buttons));
    }
}