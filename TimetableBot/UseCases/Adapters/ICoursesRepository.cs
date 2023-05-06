namespace TimetableBot.UseCases.Adapters;

public interface ICoursesRepository
{
    Task<List<string>> GetCoursesAsync();
}