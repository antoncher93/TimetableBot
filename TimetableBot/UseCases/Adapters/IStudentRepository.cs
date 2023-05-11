using TimetableBot.Models;

namespace TimetableBot.UseCases.Adapters;

public interface IStudentRepository
{
    Student? FindStudent(long userId, long chatId);

    void AddStudent(Student student);
    
    List<Student> GetAllStudents();
}