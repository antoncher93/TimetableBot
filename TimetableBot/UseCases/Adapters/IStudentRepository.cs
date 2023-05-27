using TimetableBot.Models;

namespace TimetableBot.UseCases.Adapters;

public interface IStudentRepository
{
    Task<Student?> FindStudentAsync(long userId, long chatId);

    Task AddStudentAsync(Student student);
    
    Task<List<Student>> GetAllStudentsAsync();
    
    Task SaveStudentAsAdminAsync(Student student);
}