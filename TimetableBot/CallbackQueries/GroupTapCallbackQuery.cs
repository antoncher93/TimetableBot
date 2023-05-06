namespace TimetableBot.CallbackQueries;

public class GroupTapCallbackQuery
{
    public GroupTapCallbackQuery()
    {
    }
    public GroupTapCallbackQuery(string group)
    {
        Group = group;
    }
    public string Group { get; set; }
}