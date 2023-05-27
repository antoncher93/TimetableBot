namespace TimetableBot.Infrastructure.DataModels;

public class Token
{
    public Token(string value)
    {
        Value = value;
    }
    public int Id { get; set; }
    public string Value { get; set; }
}