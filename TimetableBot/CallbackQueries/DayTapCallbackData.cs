using Newtonsoft.Json;

namespace TimetableBot.CallbackQueries;

public class DayTapCallbackData
{
    public DayTapCallbackData()
    {
    }
    
    public DayTapCallbackData(
        int course,
        int group,
        Week week,
        int dayOfWeek)
    {
        Course = course;
        Group = group;
        Week = week;
        DayOfWeek = dayOfWeek;
    }
    
    [JsonProperty(PropertyName = "c")]
    public int Course { get; set; }
    
    [JsonProperty(PropertyName = "g")]
    public int Group { get; set; }
    
    [JsonProperty(PropertyName = "w")]
    public Week Week { get; set; }
    
    [JsonProperty(PropertyName = "d")]
    public int DayOfWeek { get; set; }
}