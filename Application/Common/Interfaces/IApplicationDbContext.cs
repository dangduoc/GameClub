using Domain.Entities;
namespace Application.Common.Interfaces;
public interface IApplicationDbContext
{
    public DbSet<GameClub> GameClub { get; }
    public DbSet<GameClubEvent> GameClubEvent { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}