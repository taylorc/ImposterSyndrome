using Microsoft.EntityFrameworkCore;

namespace AppHost.Commands;

public static class PostgresDatabaseCommandExt
{
    public static IResourceBuilder<PostgresDatabaseResource> WithDropDatabaseCommand(
        this IResourceBuilder<PostgresDatabaseResource> builder)
    {
        builder.WithCommand(
            "drop-database",
            "Drop Database",
            async _ =>
            {
                var connectionString = await builder.Resource.ConnectionStringExpression.GetValueAsync(CancellationToken.None);
                ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);

                var optionsBuilder = new DbContextOptionsBuilder<DbContext>();
                optionsBuilder.UseNpgsql(connectionString);
                var db = new DbContext(optionsBuilder.Options);
                await db.Database.EnsureDeletedAsync();

                return CommandResults.Success();
            },
            null);

        return builder;
    }
}