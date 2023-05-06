using Newtonsoft.Json;

namespace TimetableBot.CallbackQueries;

public class CallbackQueryEnvelope
{
    public CallbackQueryEnvelope()
    {
    }
    
    public CallbackQueryEnvelope(
        CourseTapCallbackQuery courseTap)
    {
        CourseTap = courseTap;
    }
    
    [JsonProperty(PropertyName = "c")]
    public CourseTapCallbackQuery CourseTap { get; set; }

    public override string ToString()
    {
        return Newtonsoft.Json.JsonConvert.SerializeObject(this);
    }
}