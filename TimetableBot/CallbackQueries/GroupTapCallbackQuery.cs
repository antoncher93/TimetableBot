using Newtonsoft.Json;

namespace TimetableBot.CallbackQueries;

public class GroupTapCallbackQuery
{
    public GroupTapCallbackQuery()
    {
    }
    public GroupTapCallbackQuery(string group, string course)
    {
        Group = group;
        Course = course;
    }
    
    [JsonProperty(PropertyName = "c")]
    public string Course { get; set; }
    
    [JsonProperty(PropertyName = "g")]
    public string Group { get; set; }
}