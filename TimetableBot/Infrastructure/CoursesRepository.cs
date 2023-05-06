using TimetableBot.UseCases.Adapters;

namespace TimetableBot.Infrastructure;

public class CoursesRepository : ICoursesRepository
{
    private readonly IDataProvider _dataProvider;

    public CoursesRepository(IDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }

    public Task<List<string>> GetCoursesAsync()
    {
        var result = _dataProvider.GetCourses()
            .Select(course => course.Name)
            .ToList();

        return Task.FromResult(result);
    }
}