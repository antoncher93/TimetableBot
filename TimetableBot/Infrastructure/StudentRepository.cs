using Microsoft.EntityFrameworkCore;
using TimetableBot.Infrastructure.DataModels;
using TimetableBot.Models;
using TimetableBot.UseCases.Adapters;

namespace TimetableBot.Infrastructure;

public class StudentRepository : IStudentRepository
{
    private readonly ApplicationDbContext _db;
    public StudentRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Student?> FindStudentAsync(long userId, long chatId)
    {
        var model = await _db.Students.FirstOrDefaultAsync(
            model => model.UserId == userId
                     && model.ChatId == chatId);

        return model != null
            ? new Student(
                userId: model.UserId,
                chatId: chatId)
            : null;
    }

    public async Task AddStudentAsync(Student student)
    {
        var entity = new StudentModel()
        {
            ChatId = student.ChatId,
            UserId = student.UserId,
        };

        _db.Students.Add(entity);
        await _db.SaveChangesAsync();
    }

    public async Task<List<Student>> GetAllStudentsAsync(
        string? groupName = default)
    {
        IQueryable<StudentModel> query = _db.Students
            .Include(student => student.Group);

        if (groupName != null)
        {
            query = query.Where(student => student.Group != null && student.Group.Name == groupName);
        }
            
        var students = await query.ToListAsync();

        return students
            .Select(entity => new Student(
                userId: entity.UserId,
                chatId: entity.ChatId))
            .ToList();
    }

    public async Task SaveStudentGroupAsync(Student student, Group group)
    {
        var groupModel = await _db.Groups.FirstOrDefaultAsync(g => g.Name == group.Name);

        if (groupModel is null)
        {
            groupModel = new GroupModel(
                name: group.Name);

            _db.Groups.Add(groupModel);
            await _db.SaveChangesAsync();
        }

        var studentModel = await _db.Students
            .FirstOrDefaultAsync(s
                => s.UserId == student.UserId
                   && s.ChatId == student.ChatId);

        if (studentModel != null)
        {
            studentModel.GroupId = groupModel.Id;

            await _db.SaveChangesAsync();
        }
    }
}