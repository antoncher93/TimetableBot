using TimetableBot.CallbackQueries;
using TimetableBot.Models;

namespace TimetableBot.Infrastructure;

public class StudyDaysRepository : IStudyDaysRepository
{
    private readonly IDataProvider _dataProvider;

    public StudyDaysRepository(IDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }

    public List<StudyDay> GetDays(int course, int group, Week week)
    {
        var courseObj = _dataProvider.GetCourses()[course];
        var groupObj = courseObj.Groups[group];

        return week switch
        {
            Week.First => groupObj.Week1.Days.ToList(),
            Week.Second => groupObj.Week2.Days.ToList(),
            _ => throw new NotSupportedException(),
        };
    }
}