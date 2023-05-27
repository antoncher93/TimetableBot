namespace TimetableBot.Models;

public class Student
{
    public Student(
        long userId,
        long chatId,
        bool isAdmin)
    {
        UserId = userId;
        ChatId = chatId;
        IsAdmin = isAdmin;
    }
    
    public long UserId { get; }
    
    public long ChatId { get; }
    
    public bool IsAdmin { get; }
}