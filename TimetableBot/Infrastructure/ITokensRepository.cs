namespace TimetableBot.Infrastructure;

public interface ITokensRepository
{
    Task AddAsync(string token);
    Task<bool> ContainsTokenAsync(string token);
    Task RemoveTokenAsync(string token);
}