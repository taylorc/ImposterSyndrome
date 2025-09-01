

using ImposterSyndrome.Application.UseCases.Heroes.Commands.UpdateHero;
using ImposterSyndrome.Domain.Heroes;

namespace ImposterSyndrome.Application.UnitTests.UseCases.Heroes.Commands.UpdateHero;
public class UpdateHeroCommandHandlerTests
{
    [Fact]
    public async Task Handle_HeroExists_UpdatesHeroAndReturnsId()
    {
        // Arrange
        var heroId = Guid.NewGuid();
        var heroDomainId = HeroId.From(heroId);
        var powers = new List<Power> { new("Flight", 10) };
        var hero = Hero.Create("OldName", "OldAlias");
        hero.Id = heroDomainId;
    
        hero.UpdatePowers(powers);

        var heroes = new List<Hero> { hero };

        var dbSet = heroes.BuildMockDbSet();

        var dbContext = Substitute.For<IApplicationDbContext>();
        dbContext.Heroes.Returns(dbSet);
        dbContext.SaveChangesAsync(Arg.Any<CancellationToken>()).Returns(1);

        var handler = new UpdateHeroCommandHandler(dbContext);

        var updatePowers = new List<UpdateHeroPowerDto> { new("Strength", 8) };
        var command = new UpdateHeroCommand("NewName", "NewAlias", updatePowers) { HeroId = heroId };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(heroId, result.Value);
        Assert.Equal("NewName", hero.Name);
        Assert.Equal("NewAlias", hero.Alias);
        await dbContext.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_HeroDoesNotExist_ReturnsNotFoundError()
    {
        // Arrange
        var heroId = Guid.NewGuid();
        var heroes = new List<Hero>();

        var dbSet = heroes.BuildMockDbSet();

        var dbContext = Substitute.For<IApplicationDbContext>();
        dbContext.Heroes.Returns(dbSet);

        var handler = new UpdateHeroCommandHandler(dbContext);

        var updatePowers = new List<UpdateHeroPowerDto> { new("Strength", 20) };
        var command = new UpdateHeroCommand("NewName", "NewAlias", updatePowers) { HeroId = heroId };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsError);
        Assert.Contains(result.Errors, e => e == HeroErrors.NotFound);
        await dbContext.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
    }
}
