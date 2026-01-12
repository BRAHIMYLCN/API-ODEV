using Microsoft.EntityFrameworkCore;
using KutuphaneAPI.Models;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace KutuphaneAPI.Context;

public class AppDbContext : DbContext {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<BorrowRecord> BorrowRecords { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entityEntry in entries) {
            ((BaseEntity)entityEntry.Entity).UpdatedAt = System.DateTimeOffset.UtcNow;

            if (entityEntry.State == EntityState.Added) {
                ((BaseEntity)entityEntry.Entity).CreatedAt = System.DateTimeOffset.UtcNow;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}