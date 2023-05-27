using Microsoft.EntityFrameworkCore;
using TimetableBot.Infrastructure.DataModels;

namespace TimetableBot.Infrastructure;

public class TokensRepository : ITokensRepository
{
    private readonly ApplicationDbContext _db;

    public TokensRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(string token)
    {
        await _db.Tokens.AddAsync(new Token(token));
        await _db.SaveChangesAsync();
    }

    public async Task<bool> ContainsTokenAsync(string token)
    {
        return await _db.Tokens.AnyAsync(entity => entity.Value == token);
    }

    public async Task RemoveTokenAsync(string token)
    {
        var entity = await _db.Tokens.FirstOrDefaultAsync(entity => entity.Value == token);
        if (entity != null)
        {
            _db.Remove(entity);
            await _db.SaveChangesAsync();
        }
    }
}