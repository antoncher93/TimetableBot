using TimetableBot.Models;

namespace TimetableBot.Infrastructure;

public interface IDataProvider
{
    Task ReloadAsync();

    List<Course> GetCourses();
}