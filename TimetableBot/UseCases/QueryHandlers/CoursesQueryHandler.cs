using TimetableBot.UseCases.Adapters;
using TimetableBot.UseCases.Queries;

namespace TimetableBot.UseCases.QueryHandlers;

public class CoursesQueryHandler : ICoursesQuery
{
    private readonly ICoursesRepository _coursesRepository;

    public CoursesQueryHandler(
        ICoursesRepository coursesRepository)
    {
        _coursesRepository = coursesRepository;
    }

    public Task<List<string>> GetCoursesAsync()
    {
        return _coursesRepository.GetCoursesAsync();
    }
}