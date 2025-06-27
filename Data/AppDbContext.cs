using Microsoft.EntityFrameworkCore;
namespace Template.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        OnModelCreatingOperation(builder);
    }

    static partial void OnModelCreatingOperation(ModelBuilder builder);
}