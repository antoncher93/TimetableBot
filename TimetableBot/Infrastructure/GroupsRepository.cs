using TimetableBot.Models;
using TimetableBot.UseCases.Adapters;

namespace TimetableBot.Infrastructure;

public class GroupsRepository : IGroupsRepository
{
    private readonly IDataProvider _dataProvider;

    public GroupsRepository(IDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }

    public Task<List<Group>> GetGroupsAsync(string course)
    {
        var groups = new List<Group>();
        
        var courseObj = _dataProvider.GetCourses()
            .FirstOrDefault(c => c.Name == course);

        if (courseObj != null)
        {
            groups = courseObj.Groups.ToList();
        }

        return Task.FromResult(groups);
    }
}