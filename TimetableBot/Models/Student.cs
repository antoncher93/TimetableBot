namespace TimetableBot.Models;

public class Student
{
    public Student(long userId, long chatId)
    {
        UserId = userId;
        ChatId = chatId;
    }
    
    public long UserId { get; }
    
    public long ChatId { get; }
}