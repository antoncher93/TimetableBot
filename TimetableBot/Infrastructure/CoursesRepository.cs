using TimetableBot.UseCases.Adapters;

namespace TimetableBot.Infrastructure;

public class CoursesRepository : ICoursesRepository
{
    private readonly IDataProvider _dataProvider;

    public CoursesRepository(IDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }

    public List<string> GetCourses()
    {
        return _dataProvider.GetCourses()
            .Select(course => course.Name)
            .ToList();
    }
}