namespace TimetableBot.CallbackQueries;

public class CourseTapCallbackQuery
{
    public CourseTapCallbackQuery()
    {
    }

    public CourseTapCallbackQuery(string course)
    {
        Course = course;
    }
    
    public string Course { get; set; }
}