using TimetableBot.Models;

namespace TimetableBot.UseCases.Adapters;

public interface IStudentRepository
{
    Task<Student?> FindStudentAsync(long userId, long chatId);

    Task AddStudentAsync(Student student);
    
    Task<List<Student>> GetAllStudentsAsync(string? groupName);

    Task SaveStudentGroupAsync(Student student, Group group);
}