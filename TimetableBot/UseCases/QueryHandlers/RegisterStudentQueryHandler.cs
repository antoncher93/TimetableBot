using TimetableBot.Models;
using TimetableBot.UseCases.Adapters;
using TimetableBot.UseCases.Queries;

namespace TimetableBot.UseCases.QueryHandlers;

public class RegisterStudentQueryHandler : RegisterStudentQuery.IHandler
{
    private readonly IStudentRepository _studentRepository;

    public RegisterStudentQueryHandler(
        IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<Student> HandleAsync(
        RegisterStudentQuery query)
    {
        var student = await _studentRepository.FindStudentAsync(
            userId: query.UserId,
            chatId: query.ChatId);

        if (student is null)
        {
            student = new Student(
                userId: query.UserId,
                chatId: query.ChatId,
                isAdmin: false);

            await _studentRepository.AddStudentAsync(student);
        }

        return student;
    }
}