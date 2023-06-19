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

    public List<Group> GetGroups(int course)
    {
        var courseObj = _dataProvider.GetCourses()[course];
        return courseObj.Groups.ToList();
    }
}