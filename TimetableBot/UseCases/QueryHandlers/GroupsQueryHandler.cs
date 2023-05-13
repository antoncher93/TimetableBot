using TimetableBot.Models;
using TimetableBot.UseCases.Adapters;

namespace TimetableBot.UseCases.QueryHandlers;

public class GroupsQueryHandler : GroupsQuery.IHandler
{
    private readonly IGroupsRepository _groupsRepository;

    public GroupsQueryHandler(IGroupsRepository groupsRepository)
    {
        _groupsRepository = groupsRepository;
    }

    public Task<List<Group>> HandleAsync(GroupsQuery query)
    {
        return Task.FromResult(_groupsRepository.GetGroups(query.Course));
    }
}