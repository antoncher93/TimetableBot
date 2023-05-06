namespace TimetableBot.UseCases.Queries;

public interface ICoursesQuery
{
    Task<List<string>> GetCoursesAsync();
}