using TimetableBot.Models;

namespace TimetableBot.UseCases;

// класс для запроса групп на курсе
public class GroupsQuery
{
    public GroupsQuery(int course)
    {
        Course = course;
    }
     public interface IHandler
     {
         Task<List<Group>> HandleAsync(GroupsQuery query);
     }
    public int Course { get; }
}