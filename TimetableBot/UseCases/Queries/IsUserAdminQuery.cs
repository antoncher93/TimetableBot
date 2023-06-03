namespace TimetableBot.UseCases.Queries;

public class IsUserAdminQuery
{
    public IsUserAdminQuery(long userId)
    {
        UserId = userId;
    }
    
    public interface IHandler
    {
        Task<bool> HandleAsync(
            IsUserAdminQuery query);
    }
    
    public long UserId { get; }
}