using ImposterSyndrome.Application.Common.Interfaces;
using ImposterSyndrome.Domain.Accessories;
using ImposterSyndrome.Domain.Common.Interfaces;
using ImposterSyndrome.Domain.Heroes;
using ImposterSyndrome.Domain.Programme.Wendler531;
using ImposterSyndrome.Domain.Teams;
using ImposterSyndrome.Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;
using SmartEnum.EFCore;
using System.Reflection;
using System.Reflection.Emit;

namespace ImposterSyndrome.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions options)
    : DbContext(options), IApplicationDbContext
{
    public DbSet<Hero> Heroes => AggregateRootSet<Hero>();

    public DbSet<Team> Teams => AggregateRootSet<Team>();

    public DbSet<Accessory> Accessories => AggregateRootSet<Accessory>();

    public DbSet<Wendler531Programme> Wendler531Programmes => AggregateRootSet<Wendler531Programme>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        configurationBuilder.ConfigureSmartEnum();

        configurationBuilder.RegisterAllInVogenEfCoreConverters();
    }

    private DbSet<T> AggregateRootSet<T>() where T : class, IAggregateRoot => Set<T>();
}