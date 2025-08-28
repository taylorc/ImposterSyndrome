using ImposterSyndrome.Domain.Heroes;
using ImposterSyndrome.Domain.Teams;

namespace ImposterSyndrome.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Hero> Heroes { get; }
    DbSet<Team> Teams { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}