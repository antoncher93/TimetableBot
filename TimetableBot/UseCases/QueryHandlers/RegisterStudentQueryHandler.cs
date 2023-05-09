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

    public Task<Student> HandleAsync(
        RegisterStudentQuery query)
    {
        var student = _studentRepository.FindStudent(
            userId: query.UserId,
            chatId: query.ChatId);

        if (student is null)
        {
            student = new Student(
                userId: query.UserId,
                chatId: query.ChatId);

            _studentRepository.AddStudent(student);
        }

        return Task.FromResult(student);
    }
}