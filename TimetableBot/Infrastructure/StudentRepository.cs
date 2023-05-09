using TimetableBot.Models;
using TimetableBot.UseCases.Adapters;

namespace TimetableBot.Infrastructure;

public class StudentRepository : IStudentRepository
{
    private readonly IDictionary<string, Student> _students = new Dictionary<string, Student>();

    public StudentRepository()
    {
    }

    public Student? FindStudent(long userId, long chatId)
    {
        var exists = _students.TryGetValue(BuildKey(userId, chatId), out var value);
        return exists ? value : null;
    }

    public async void AddStudent(Student student)
    {
        var key = BuildKey(
            userId: student.UserId,
            chatId: student.ChatId);
        
        _students[key] = student;
    }

    private string BuildKey(long userId, long chatId)
    {
        return $"{userId}:{chatId}";
    }
}