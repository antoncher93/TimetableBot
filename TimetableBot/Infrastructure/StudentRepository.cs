using Dapper;
using TimetableBot.Models;
using TimetableBot.UseCases.Adapters;

namespace TimetableBot.Infrastructure;

public class StudentRepository : IStudentRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public StudentRepository(
        IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<Student?> FindStudentAsync(long userId, long chatId)
    {
        using var connection = _dbConnectionFactory.Create();
        var data = await connection.QueryFirstOrDefaultAsync<DataModels.StudentData>(
            sql: $"SELECT * FROM Students WHERE UserId={userId} AND ChatId={chatId}");

        return data != null
            ? new Student(data.UserId, data.ChatId)
            : null;
    }

    public async Task AddStudentAsync(Student student)
    {
        using var connection = _dbConnectionFactory.Create();
        await connection.ExecuteAsync(
            sql: $"INSERT INTO Students (UserId, ChatId) VALUES ({student.UserId}, {student.ChatId})");
    }
}