using TimetableBot.Models;

namespace TimetableBot.UseCases.Adapters;

public interface IGroupsRepository
{
    List<Group> GetGroups(int course);
}