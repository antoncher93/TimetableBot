using TimetableBot.Infrastructure;
using TimetableBot.Models;
using TimetableBot.UseCases.Adapters;
using TimetableBot.UseCases.Queries;

namespace TimetableBot.UseCases.QueryHandlers;

public class RegisterStudentQueryHandler : RegisterStudentQuery.IHandler
{
    private readonly IStudentRepository _studentRepository;
    private readonly IAdminRepository _adminRepository;
    
    public RegisterStudentQueryHandler(
        IStudentRepository studentRepository,
        IAdminRepository adminRepository)
    {
        _studentRepository = studentRepository;
        _adminRepository = adminRepository;
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
                chatId: query.ChatId);

            await _studentRepository.AddStudentAsync(student);
        }

        await this.AddAdminIfStudentFirstAsync(userId: student.UserId);

        return student;
    }

    private async Task AddAdminIfStudentFirstAsync(long userId)
    {
        var noAdmins = await _adminRepository.IsEmptyAsync();

        if (noAdmins)
        {
            var token = Guid.NewGuid().ToString();
            await _adminRepository.UpsertAdminAsync(token, userId);
        }
    }
}