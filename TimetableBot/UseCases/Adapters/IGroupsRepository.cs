using TimetableBot.Models;

namespace TimetableBot.UseCases.Adapters;

public interface IGroupsRepository
{
    Task<List<Group>> GetGroupsAsync(int course);
}