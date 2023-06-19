namespace TimetableBot.UseCases.Adapters;

public interface ICoursesRepository
{
    List<string> GetCourses();
}