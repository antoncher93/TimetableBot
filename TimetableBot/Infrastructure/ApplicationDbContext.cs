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
    
    public DbSet<Admin> Admins { get; set; }
    
    public DbSet<GroupModel> Groups { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<StudentModel>(entity =>
        {
            entity.HasOne(s => s.Group)
                .WithMany(g => g.Students)
                .HasForeignKey(s => s.GroupId);
        });
    }
}