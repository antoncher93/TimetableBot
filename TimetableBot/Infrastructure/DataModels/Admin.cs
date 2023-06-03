namespace TimetableBot.Infrastructure.DataModels;

public class Admin
{
    public Admin(string secretKey, long? userId = default)
    {
        UserId = userId;
        SecretKey = secretKey;
    }
    
    public int Id { get; set; }
    
    public long? UserId { get; set; }
    
    public string SecretKey { get; set; }
}