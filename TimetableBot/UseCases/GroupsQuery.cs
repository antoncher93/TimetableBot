using TimetableBot.Models;

namespace TimetableBot.UseCases;

// класс для запроса групп на курсе
public class GroupsQuery
{
    public GroupsQuery(string course)
    {
        Course = course;
    }
     public interface IHandler
     {
         Task<List<Group>> HandleAsync(GroupsQuery query);
     }
    public string Course { get; }
}