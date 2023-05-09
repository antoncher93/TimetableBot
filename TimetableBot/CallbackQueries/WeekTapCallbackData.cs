using Newtonsoft.Json;

namespace TimetableBot.CallbackQueries;

public class WeekTapCallbackData
{
    public WeekTapCallbackData()
    {
    }

    public WeekTapCallbackData(int course, int group, Week week)
    {
        Course = course;
        Group = group;
        Week = week;
    }

    [JsonProperty(PropertyName = "c")]
    public int Course { get; set; }
    
    [JsonProperty(PropertyName = "g")]
    public int Group { get; set; }
    
    [JsonProperty(PropertyName = "w")]
    public Week Week { get; set; }
}