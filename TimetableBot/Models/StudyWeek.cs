namespace TimetableBot.Models;

public class StudyWeek
{
    public StudyWeek(List<StudyDay> days)
    {
        Days = days;
    }
    
    public List<StudyDay> Days { get; }
}