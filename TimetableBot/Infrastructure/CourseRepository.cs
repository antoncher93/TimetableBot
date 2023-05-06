using TimetableBot.UseCases.Adapters;

namespace TimetableBot.Infrastructure;

public class CourseRepository : ICoursesRepository
{
    private readonly YandexCloudDataProvider _yandexCloudDataProvider;

    public CourseRepository(
        YandexCloudDataProvider yandexCloudDataProvider)
    {
        _yandexCloudDataProvider = yandexCloudDataProvider;
    }

    public Task<List<string>> GetCoursesAsync()
    {
        var courses = _yandexCloudDataProvider
            .GetCourses()
            .Select(c => c.Name)
            .ToList();

        return Task.FromResult(courses);
    }
}