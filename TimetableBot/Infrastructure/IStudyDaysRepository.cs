using TimetableBot.CallbackQueries;
using TimetableBot.Models;

namespace TimetableBot.Infrastructure;

public interface IStudyDaysRepository
{
    List<StudyDay> GetDays(int course, int group, Week week);
}