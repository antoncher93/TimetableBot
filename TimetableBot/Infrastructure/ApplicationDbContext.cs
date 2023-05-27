using Microsoft.EntityFrameworkCore;
using TimetableBot.Infrastructure.DataModels;

namespace TimetableBot.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        this.Database.EnsureCreated();
    }
    public DbSet<StudentModel> Students { get; set; }
    
    public DbSet<Token> Tokens { get; set; }
}