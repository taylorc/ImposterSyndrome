using ImposterSyndrome.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Respawn;
using System.Data.Common;

namespace WebApi.IntegrationTests.Common.Infrastructure.Database;

public class TestDatabase : IAsyncDisposable
{
    private readonly PostgresContainer _postgres = new();
    private Respawner _checkpoint = null!;
    private string _connectionString = null!;

    public async Task InitializeAsync()
    {
        await _postgres.InitializeAsync();

        var builder = new NpgsqlConnectionStringBuilder(_postgres.Connection!.ConnectionString)
        {
            Database = "CleanArchitecture-IntegrationTests"
        };

        _connectionString = builder.ConnectionString;

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql(_connectionString)
            .Options;

        using var dbContext = new ApplicationDbContext(options);
        await dbContext.Database.MigrateAsync();

        await using var connection = DbConnection;
        await connection.OpenAsync();
        _checkpoint = await Respawner.CreateAsync(connection,
            new RespawnerOptions
            {
                TablesToIgnore = ["__EFMigrationsHistory"],
                DbAdapter = DbAdapter.Postgres
            });
    }

    public DbConnection DbConnection => new NpgsqlConnection(_connectionString);

    public async Task ResetAsync()
    {
        await using var connection = DbConnection;
        await connection.OpenAsync();
        await _checkpoint.ResetAsync(connection);
    }

    public async ValueTask DisposeAsync()
    {
        await _postgres.DisposeAsync();
    }
}