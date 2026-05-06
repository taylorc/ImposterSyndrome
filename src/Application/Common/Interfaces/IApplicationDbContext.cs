using ImposterSyndrome.Domain.Accessories;
using ImposterSyndrome.Domain.Heroes;
using ImposterSyndrome.Domain.Teams;

namespace ImposterSyndrome.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Hero> Heroes { get; }
    DbSet<Team> Teams { get; }

    DbSet<Accessory> Accessories { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}