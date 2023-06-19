namespace TimetableBot.Infrastructure.DataModels;

public class StudentModel
{
    public int Id { get; set; }
    
    public long UserId { get; set; }
    
    public long ChatId { get; set; }
    
    public int? GroupId { get; set; }
    
    public GroupModel? Group { get; set; }
}