namespace TimetableBot.CallbackQueries;

public class CourseTapCallbackData
{
    public CourseTapCallbackData()
    {
    }

    public CourseTapCallbackData(int course)
    {
        Course = course;
    }
    
    public int Course { get; set; }
}