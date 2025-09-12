

using ImposterSyndrome.Application.UseCases.Heroes.Commands.UpdateHero;
using ImposterSyndrome.Domain.Heroes;


namespace ImposterSyndrome.Application.UnitTests.UseCases.Heroes.Commands.UpdateHero;
public class UpdateHeroCommandHandlerTests
{
    private UpdateHeroCommandValidator _validator = new();
    private Faker _faker = new Faker();

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

    [Fact]
    public void Should_Have_Error_When_HeroId_Is_Empty()
    {
        var command = new UpdateHeroCommand("ValidName", "ValidAlias", [])
        {
            HeroId = Guid.Empty,
            Name = _faker.Lorem.Word(),
            Alias = _faker.Lorem.Word(),
            Powers = _faker.Make(2, () => new UpdateHeroPowerDto(_faker.Lorem.Word(), _faker.Random.Number(0, 10)))
        };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.HeroId);
    }

    [Fact]
    public void Should_Have_Error_When_Name_Is_Empty()
    {
        

        var command = new UpdateHeroCommand("", "ValidAlias", [])
        {
            HeroId = Guid.NewGuid(),
            Alias = _faker.Lorem.Word(),
            Powers = _faker.Make(2, () => new UpdateHeroPowerDto(_faker.Lorem.Word(), _faker.Random.Number(0, 10)))
        };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.Name);
    }

    [Fact]
    public void Should_Have_Error_When_Alias_Is_Empty()
    {
        var command = new UpdateHeroCommand("ValidName", "", [])
        {
            HeroId = Guid.NewGuid(),
            Name = _faker.Lorem.Word(),
            Powers = _faker.Make(2, () => new UpdateHeroPowerDto(_faker.Lorem.Word(), _faker.Random.Number(0, 10)))
        };

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(c => c.Alias);
    }

    [Fact]
    public void Should_Not_Have_Error_When_All_Fields_Are_Valid()
    {
        var command = new UpdateHeroCommand("ValidName", "ValidAlias", [])
        {
            HeroId = Guid.NewGuid(),
            Name = _faker.Lorem.Word(),
            Alias = _faker.Lorem.Word(),
            Powers = _faker.Make(2, () => new UpdateHeroPowerDto(_faker.Lorem.Word(), _faker.Random.Int(0, 10)))

        };

        var result = _validator.TestValidate(command);

        result.ShouldNotHaveAnyValidationErrors();
    }
}
