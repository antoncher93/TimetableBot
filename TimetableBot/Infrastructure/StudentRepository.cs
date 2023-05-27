﻿using Microsoft.EntityFrameworkCore;
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
                chatId: chatId,
                isAdmin: model.IsAdmin)
            : null;
    }

    public async Task AddStudentAsync(Student student)
    {
        var studentCount = await _db.Students.CountAsync();

        var entity = new StudentModel()
        {
            ChatId = student.ChatId,
            UserId = student.UserId,
            IsAdmin = studentCount == 0,
        };

        _db.Students.Add(entity);
        await _db.SaveChangesAsync();
    }

    public async Task<List<Student>> GetAllStudentsAsync()
    {
        var allEntities = await _db.Students
            .ToListAsync();

        return allEntities
            .Select(entity => new Student(
                userId: entity.UserId,
                chatId: entity.ChatId,
                isAdmin: entity.IsAdmin))
            .ToList();
    }

    public async Task SaveStudentAsAdminAsync(Student student)
    {
        var entity = await _db.Students
            .FirstOrDefaultAsync(model => model.UserId == student.UserId
                                          && model.ChatId == student.ChatId);

        entity.IsAdmin = true;
        
        await _db.SaveChangesAsync();
    }
}