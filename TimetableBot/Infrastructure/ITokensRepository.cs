namespace TimetableBot.Infrastructure;

public interface ITokensRepository
{
    void Add(string token);
    bool Contains(string token);
    void Remove(string token);
}