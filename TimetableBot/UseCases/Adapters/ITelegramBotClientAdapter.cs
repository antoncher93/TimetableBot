using TimetableBot.CallbackQueries;
using TimetableBot.Models;

namespace TimetableBot.UseCases.Adapters;

public interface ITelegramBotClientAdapter
{
    Task ShowCoursesAsync(
        Student student,
        List<string> courses);

    Task ShowGroupsAsync(
        Student student,
        List<Group> groups,
        int course);

    Task ShowWeeksAsync(
        Student student,
        int course,
        int group);

    Task ShowDaysAsync(
        Student student,
        int course,
        int group,
        Week week,
        List<string> days);

    Task ShowTimetableAsync(long chatId, List<StudyDay> days);
}