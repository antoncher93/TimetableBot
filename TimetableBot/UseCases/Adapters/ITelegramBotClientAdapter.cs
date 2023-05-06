using TimetableBot.Models;

namespace TimetableBot.UseCases.Adapters;

public interface ITelegramBotClientAdapter
{
    Task ShowCoursesAsync(
        Student student,
        List<string> courses);
}