using Npgsql;
using Polly;
using Testcontainers.PostgreSql;

namespace WebApi.IntegrationTests.Common.Infrastructure.Database;

public class PostgresContainer : IAsyncDisposable
{
    private readonly PostgreSqlContainer _container = new PostgreSqlBuilder()
        .WithName($"CleanArchitecture-IntegrationTests-{Guid.NewGuid()}")
        .WithPassword("Password123")
        .WithAutoRemove(true)
        .Build();

    private const int MaxRetries = 5;

    public NpgsqlConnection? Connection { get; private set; }

    public async Task InitializeAsync()
    {
        await StartWithRetry();
        Connection = new NpgsqlConnection(_container.GetConnectionString());
    }

    private async Task StartWithRetry()
    {
        var policy = Policy.Handle<InvalidOperationException>()
            .WaitAndRetryAsync(MaxRetries, _ => TimeSpan.FromSeconds(5));

        await policy.ExecuteAsync(async () => { await _container.StartAsync(); });
    }

    public async ValueTask DisposeAsync()
    {
        await _container.StopAsync();
        await _container.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}