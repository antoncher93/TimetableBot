using TimetableBot.Models;

namespace TimetableBot.Infrastructure;

public interface IExcelFileReader
{
    Task<Course> ReadCourseDataFromBytesAsync(byte[] bytes);
}