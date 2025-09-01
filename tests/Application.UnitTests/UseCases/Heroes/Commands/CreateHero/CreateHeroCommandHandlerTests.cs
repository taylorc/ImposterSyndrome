using ImposterSyndrome.Application.UseCases.Heroes.Commands.CreateHero;
using ImposterSyndrome.Domain.Heroes;
using NSubstitute.ExceptionExtensions;
using System.Collections.Generic;

namespace ImposterSyndrome.Application.UnitTests.UseCases.Heroes.Commands.CreateHero;
public class CreateHeroCommandHandlerTests
{
    [Fact]
    public async Task Handle_ValidRequest_AddsHeroAndSavesChanges_ReturnsHeroId()
    {
        // Arrange
        var dbSet = Substitute.For<DbSet<Hero>, IQueryable<Hero>>();
        var dbContext = Substitute.For<IApplicationDbContext>();
        dbContext.Heroes.Returns(dbSet);
        dbContext.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(1);

        dbSet.AddAsync(Arg.Any<Hero>(), Arg.Any<CancellationToken>())
            .Returns(ValueTask.FromResult((Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Hero>)null!));

        var handler = new CreateHeroCommandHandler(dbContext);

        var powers = new List<CreateHeroPowerDto>
        {
            new("Flight", 10),
            new("Strength", 8)
        };

        var command = new CreateHeroCommand("Clark Kent", "Superman", powers);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        await dbSet.Received(1).AddAsync(Arg.Any<Hero>(), Arg.Any<CancellationToken>());
        await dbContext.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());

//        Assert.True(result.IsOk);
        Assert.IsType<Guid>(result.Value);
    }

    [Fact]
    public async Task Handle_SaveChangesAsyncFails_ThrowsException()
    {
        // Arrange
        var dbSet = Substitute.For<DbSet<Hero>, IQueryable<Hero>>();
        var dbContext = Substitute.For<IApplicationDbContext>();
        dbContext.Heroes.Returns(dbSet);
        dbContext.SaveChangesAsync(Arg.Any<CancellationToken>())
            .ThrowsAsync(new DbUpdateException("Save failed"));

        dbSet.AddAsync(Arg.Any<Hero>(), Arg.Any<CancellationToken>())
            .Returns(ValueTask.FromResult((Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Hero>)null!));

        var handler = new CreateHeroCommandHandler(dbContext);

        var powers = new List<CreateHeroPowerDto>
        {
            new("Invisibility", 5)
        };

        var command = new CreateHeroCommand("Bruce Wayne", "Batman", powers);

        // Act & Assert
        await Assert.ThrowsAsync<DbUpdateException>(() => handler.Handle(command, CancellationToken.None));
    }
}
