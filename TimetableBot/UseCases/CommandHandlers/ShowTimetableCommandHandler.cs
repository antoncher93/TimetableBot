using TimetableBot.Infrastructure;
using TimetableBot.Models;
using TimetableBot.UseCases.Adapters;
using TimetableBot.UseCases.Commands;

namespace TimetableBot.UseCases.CommandHandlers;

public class ShowTimetableCommandHandler : ShowTimetableCommand.IHandler
{
    private readonly ITelegramBotClientAdapter _adapter;
    private readonly IStudyDaysRepository _studyDaysRepository;

    public ShowTimetableCommandHandler(
        ITelegramBotClientAdapter adapter,
        IStudyDaysRepository studyDaysRepository)
    {
        _adapter = adapter;
        _studyDaysRepository = studyDaysRepository;
    }

    public Task HandleAsync(ShowTimetableCommand command)
    {
        return command.Type switch
        {
            TimetableType.Week => this.ShowWeekTimeTableAsync(command),
            TimetableType.Tomorrow => this.ShowTomorrowTimeTableAsync(command),
            TimetableType.Today => this.ShowTodayTimetableAsync(command),
            _ => Task.CompletedTask,
        };
    }

    private Task ShowTodayTimetableAsync(ShowTimetableCommand command)
    {
        if (IsHoliday(DateTime.Today))
        {
            return _adapter.ShowNoTimetableAsync(
                chatId: command.ChatId,
                course: command.Course,
                group: command.Group);
        }
        return ShowTimetableAsync(
            date: DateTime.Today, 
            course: command.Course,
            group: command.Group,
            chatId: command.ChatId);
    }

    private Task ShowWeekTimeTableAsync(ShowTimetableCommand command)
    {
        var today = DateTime.Today;

        if (IsHoliday(today))
        {
            return _adapter.ShowNoTimetableAsync(
                chatId: command.ChatId,
                course: command.Course,
                group: command.Group);
        }
        
        var days = _studyDaysRepository
            .GetDays(
                course: command.Course,
                group: command.Group,
                week: command.Week!.Value);
        
        return _adapter.ShowTimetableAsync(
            chatId: command.ChatId,
            days: days,
            course: command.Course,
            group: command.Group);
    }

    private Task ShowTomorrowTimeTableAsync(ShowTimetableCommand command)
    {
        var tomorrow = DateTime.Today.AddDays(1);

        if (IsHoliday(tomorrow))
        {
            return _adapter.ShowNoTimetableAsync(
                chatId: command.ChatId,
                group: command.Group,
                course: command.Course);
        }

        return ShowTimetableAsync(
            date: tomorrow,
            course: command.Course,
            group: command.Group,
            chatId: command.ChatId);
    }

    private Task ShowTimetableAsync(
        DateTime date,
        int course,
        int group,
        long chatId)
    {
        var week = this.CalcCurrentWeek(date);

        var dayOfWeek = this.MapDayOfWeek(date);
        
        var days = _studyDaysRepository
            .GetDays(
                course: course,
                group: group,
                week: week);

        var studyDay = days.FirstOrDefault(d => d.DayOfWeek.ToLower() == dayOfWeek);

        if (studyDay is null)
        {
            return _adapter.ShowNoTimetableForDayAsync(
                chatId: chatId,
                date: date, 
                course: course, group: group);
        }
        else
        {
            return _adapter.ShowDayTimetableAsync(
                chatId: chatId,
                date: date,
                studyDay: studyDay, course: course, group: group);
        }
    }

    private Week CalcCurrentWeek(DateTime date)
    {
        var year = DateTime.Now.Month < 9    // если текущий месяц идет раньше сентября
            ? DateTime.Now.AddYears(-1).Year    // берем прошлый год
            : DateTime.Now.Year;                // берем текущий год
        
        var firstSept = new DateTime(
            year: year,
            month: 9,
            day: 1);

        var startDay = firstSept.AddDays(7 + (int)DayOfWeek.Monday - (int)firstSept.DayOfWeek);

        var weeksCount = ((int)(date - startDay).TotalDays) / 7;

        return weeksCount % 2 == 0
            ? Week.Second
            : Week.First;
    }

    private string MapDayOfWeek(DateTime day)
    {
        return day.DayOfWeek switch
        {
            DayOfWeek.Monday => "понедельник",
            DayOfWeek.Tuesday => "вторник",
            DayOfWeek.Wednesday => "среда",
            DayOfWeek.Thursday => "четверг",
            DayOfWeek.Friday => "пятница",
            DayOfWeek.Saturday => "суббота",
            DayOfWeek.Sunday => "воскресенье",
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private bool IsHoliday(DateTime dateTime)
    {
        return dateTime.Month >= 6
               && dateTime.Month <= 9;
    }
}