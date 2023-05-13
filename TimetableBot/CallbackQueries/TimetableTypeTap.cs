using Newtonsoft.Json;
using TimetableBot.Models;

namespace TimetableBot.CallbackQueries;

public class TimetableTypeTap
{
    public TimetableTypeTap()
    {
    }

    public TimetableTypeTap(int course, int group, TimetableType type)
    {
        Course = course;
        Group = group;
        Type = type;
    }
    
    [JsonProperty(PropertyName = "c")]
    public int Course { get; set; }
    
    [JsonProperty(PropertyName = "g")]
    public int Group { get; set; }
    
    [JsonProperty(PropertyName = "t")]
    public TimetableType Type { get; set; }
}