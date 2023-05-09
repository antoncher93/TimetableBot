namespace TimetableBot.Models;

public class Student
{
    public Student(long userId, long chatId)
    {
        UserId = userId;
        ChatId = chatId;
        IsAdmin = false;
    }
    
    public long UserId { get; }
    
    public long ChatId { get; }
    
    public bool IsAdmin { get; set; }
}