using Newtonsoft.Json;

namespace TimetableBot.CallbackQueries;

public class GroupTapCallbackData
{
    public GroupTapCallbackData()
    {
    }
    public GroupTapCallbackData(int group, int course)
    {
        Group = group;
        Course = course;
    }
    
    [JsonProperty(PropertyName = "c")]
    public int Course { get; set; }
    
    [JsonProperty(PropertyName = "g")]
    public int Group { get; set; }
}