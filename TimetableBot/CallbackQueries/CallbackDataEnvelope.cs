using Newtonsoft.Json;

namespace TimetableBot.CallbackQueries;

public class CallbackDataEnvelope
{
    public CallbackDataEnvelope()
    {
    }
    
    public CallbackDataEnvelope(
        CourseTapCallbackData courseTap)
    {
        CourseTap = courseTap;
    }

    public CallbackDataEnvelope(
        GroupTapCallbackData groupTap)
    {
        GroupTap = groupTap;
    }

    public CallbackDataEnvelope(
        WeekTapCallbackData weekTap)
    {
        WeekTap = weekTap;
    }

    public CallbackDataEnvelope(
        DayTapCallbackData dayTap)
    {
        DayTap = dayTap;
    }

    public CallbackDataEnvelope(
        MainMenuCallbackData mainMenu)
    {
        MainMenu = mainMenu;
    }

    public CallbackDataEnvelope(
        TimetableTypeTap timetableTypeTap)
    {
        this.TimetableTypeTap = timetableTypeTap;
    }
    
    [JsonProperty(PropertyName = "c")]
    public CourseTapCallbackData? CourseTap { get; set; }
    
    [JsonProperty(PropertyName = "g")]
    public GroupTapCallbackData? GroupTap { get; set; }
    
    [JsonProperty(PropertyName = "w")]
    public WeekTapCallbackData? WeekTap { get; set; }
    
    [JsonProperty(PropertyName = "d")]
    public DayTapCallbackData? DayTap { get; set; }
    
    [JsonProperty(PropertyName = "m")]
    public MainMenuCallbackData? MainMenu { get; set; }
    
    [JsonProperty(PropertyName = "t")]
    public TimetableTypeTap? TimetableTypeTap { get; set; }

    // метод для матчинг-паттерна
    public T Match<T>(
        Func<CourseTapCallbackData, T> onCourseTap,
        Func<GroupTapCallbackData, T> onGroupTap,
        Func<TimetableTypeTap, T> onTimetableTypeTap,
        Func<WeekTapCallbackData, T> onWeekTap,
        Func<MainMenuCallbackData, T> onMainMenu)
    {
        if (this.CourseTap != null)
        {
            return onCourseTap(this.CourseTap);
        }

        if (this.GroupTap != null)
        {
            return onGroupTap(this.GroupTap);
        }

        if (this.WeekTap != null)
        {
            return onWeekTap(this.WeekTap);
        }

        if (this.MainMenu != null)
        {
            return onMainMenu(this.MainMenu);
        }

        if (this.TimetableTypeTap != null)
        {
            return onTimetableTypeTap(this.TimetableTypeTap);
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