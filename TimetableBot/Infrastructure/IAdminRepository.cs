namespace TimetableBot.Infrastructure;

public interface IAdminRepository
{
    Task AddAsync(string token);
    
    Task<bool> ContainsFreeTokenAsync(string token);
    
    Task<bool> ContainsUserAsync(long userId);

    Task UpsertAdminAsync(string token, long userId);
    
    Task RemoveTokenAsync(string token);
    
    Task<bool> IsEmptyAsync();
}