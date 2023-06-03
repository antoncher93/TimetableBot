using Microsoft.EntityFrameworkCore;
using TimetableBot.Infrastructure.DataModels;

namespace TimetableBot.Infrastructure;

public class AdminRepository : IAdminRepository
{
    private readonly ApplicationDbContext _db;

    public AdminRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(string token)
    {
        await _db.Admins.AddAsync(new Admin(
            secretKey: token));
        
        await _db.SaveChangesAsync();
    }

    public async Task<bool> ContainsFreeTokenAsync(string token)
    {
        return await _db.Admins.AnyAsync(entity => entity.SecretKey == token);
    }

    public Task<bool> ContainsUserAsync(long userId)
    {
        return _db.Admins.AnyAsync(admin => admin.UserId == userId);
    }

    public async Task UpsertAdminAsync(string token, long userId)
    {
        var admin = await _db.Admins.FirstOrDefaultAsync(
            admin => admin.SecretKey == token);

        if (admin is null)
        {
            admin = new Admin(
                secretKey: token,
                userId: userId);

            await _db.Admins.AddAsync(admin);
        }
        else
        {
            admin.UserId = userId;
        }

        await _db.SaveChangesAsync();
    }

    public async Task RemoveTokenAsync(string token)
    {
        var entity = await _db.Admins.FirstOrDefaultAsync(entity => entity.SecretKey == token);
        if (entity != null)
        {
            _db.Remove(entity);
            await _db.SaveChangesAsync();
        }
    }

    public async Task<bool> IsEmptyAsync()
    {
        var count = await _db.Admins.CountAsync();
        return count == 0;
    }
}