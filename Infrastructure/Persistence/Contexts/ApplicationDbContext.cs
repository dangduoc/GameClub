using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Persistence.Contexts;
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly TimeProvider _dateTime;
    public ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options, TimeProvider dateTime) : base(options)
    {
        _dateTime = dateTime;
    }

 
    public DbSet<GameClub> GameClub => Set<GameClub>();
    public DbSet<GameClubEvent> GameClubEvent => Set<GameClubEvent>();

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            var utcNow = _dateTime.GetUtcNow();
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = utcNow.DateTime;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = utcNow.DateTime;
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

    }
}