namespace TimetableBot.Infrastructure;

public class TokensRepository : ITokensRepository
{
    private readonly HashSet<string> _tokens = new HashSet<string>();
    public void Add(string token)
    {
        _tokens.Add(token);
    }

    public bool Contains(string token)
    {
        return _tokens.Contains(token);
    }

    public void Remove(string token)
    {
        _tokens.Remove(token);
    }
}