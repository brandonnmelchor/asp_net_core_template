using Microsoft.EntityFrameworkCore;
using Template.Components.Operation;
namespace Template.Data;

public partial class AppDbContext
{
    public DbSet<Operation> Operations { get; set; }

    static partial void OnModelCreatingOperation(ModelBuilder builder)
    {
        builder.Entity<Operation>(builder =>
        {
            builder.ToTable(AppDb.Operation);
            builder.HasKey(operation => operation.Id);
        });

    }
}