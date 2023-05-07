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

    public CallbackQueryEnvelope(
        GroupTapCallbackQuery groupTap)
    {
        GroupTap = groupTap;
    }
    
    [JsonProperty(PropertyName = "c")]
    public CourseTapCallbackQuery? CourseTap { get; set; }
    
    [JsonProperty(PropertyName = "g")]
    public GroupTapCallbackQuery? GroupTap { get; set; }

    // метод для матчинг-паттерна
    public T Match<T>(
        Func<CourseTapCallbackQuery, T> onCourseTap,
        Func<GroupTapCallbackQuery, T> onGroupTap)
    {
        if (this.CourseTap != null)
        {
            return onCourseTap(this.CourseTap);
        }

        if (this.GroupTap != null)
        {
            return onGroupTap(this.GroupTap);
        }

        throw new NotSupportedException();
    }

    public override string ToString()
    {
        return Newtonsoft.Json.JsonConvert.SerializeObject(
            value: this,
            settings: new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
            });
    }
}